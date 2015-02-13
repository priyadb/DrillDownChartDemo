<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DrillDownChartDemo.Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Drill-Down Chart Demo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:SqlDataSource runat="server" ID="chartsource" ConnectionString="server=localhost;database=test;uid=root;pwd=priya123;" ProviderName="MySql.Data.MySqlClient" SelectCommand="select * from test" />
    
        <asp:Chart ID="Chart2" runat="server" Height="296px" 
            Width="438px"  imagetype="Png" BackGradientStyle="LeftRight" BackColor="AliceBlue" Palette="Pastel" > 
            <borderskin skinstyle="Emboss"></borderskin>
            <series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
        </asp:Chart>
    </div>
    </form>
</body>
</html>
