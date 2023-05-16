﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebReportDesigner1.aspx.cs" Inherits="Portal_Apogee.WebReportDesigner1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Telerik Web Report Designer</title>

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500&display=swap" rel="stylesheet" />
</head>

<body>
    <div id="webReportDesigner">
        loading...
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2020.3.1118/js/kendo.all.min.js"></script>

    <script src="/api/reportdesigner/resources/js/telerikReportViewer/"></script>
    <script src="/api/reportdesigner/designerresources/js/webReportDesigner/"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#webReportDesigner").telerik_WebReportDesigner({
                toolboxArea: {
                    layout: "list" //Change to "grid" to display the contents of the Components area in a flow grid layout.
                },
                serviceUrl: "/api/reportdesigner",
                report: "SampleReport.trdp"
            }).data("telerik_WebDesigner");
        });
    </script>
    <form id="form1" runat="server">
    </form>
</body>
</html>
