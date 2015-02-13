using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace DrillDownChartDemo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Connect to MySql database and retrieve data from the tables
            string cs = "Server=localhost;Database=test;Uid=root;Pwd=priya123;";
            MySqlConnection cnx = new MySqlConnection(cs);
            string reg = "SELECT * FROM regiontable";
            string sales = "SELECT * FROM salesreptable";
            MySqlDataAdapter adapter = new MySqlDataAdapter(reg, cnx);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "regiontable");
            DataTable regiontable = ds.Tables[0];
            adapter = new MySqlDataAdapter(sales, cnx);
            ds = new DataSet();
            adapter.Fill(ds, "salesreptable");
            DataTable salesrep = ds.Tables[0];

            //Create new series to populate the chart with data
            Series series = new Series("Column");
            series.ChartType = SeriesChartType.Column;
            
            string region1 = "";
            //if page request value for "region" is not null,
            //then populate chart with data for sales by each representative
            //else populate chart with region specific data
            if (this.Page.Request["region"] != null)
            {
                region1 = (string)this.Page.Request["region"];
                var query = from reps in salesrep.AsEnumerable()
                            join regions in regiontable.AsEnumerable()
                            on reps.Field<int>("regionid") equals regions.Field<int>("regionid")
                            where regions.Field<string>("regionname") == (Page.Request["Region"] ?? "East")
                            select new { RepName = reps.Field<string>("name"), Sales = reps.Field<int>("sales") };
                

                foreach (var value in query)
                {
                    series.Points.AddXY(value.RepName, value.Sales);
                }
                Chart2.Series.Add(series);
                Chart2.Palette= ChartColorPalette.Fire;
                Chart2.Titles.Add("Sales by each Representative");
            }
            else
            {
                var query = from reps in salesrep.AsEnumerable()
                            join regions in regiontable.AsEnumerable()
                            on reps.Field<int>("regionid") equals regions.Field<int>("regionid")
                            group reps by regions.Field<string>("regionname") into regionGroup
                            select new { Region = regionGroup.Key, Sales = regionGroup.Sum(total => total.Field<int>("Sales")) };

                foreach (var value in query)
                {
                    series.Points.AddXY(value.Region, value.Sales);
                }
                for (int i = 0; i < series.Points.Count; i++)
                {
                    series.Points[i].Url = string.Format("Default.aspx?region={0}", series.Points[i].AxisLabel);
                }
                Chart2.Series.Add(series);
                Chart2.Titles.Add("Sales for each Region");
        }
        }
    }
}