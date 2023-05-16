<%@ Page Title="Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Portal_Apogee.View" %>

<%@ Register Assembly="Telerik.ReportViewer.Html5.WebForms, Version=15.2.21.915, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.Html5.WebForms" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        table {
            max-width: none;
            background-color: transparent;
            border-collapse: collapse;
            border-spacing: 0;
        }

        .table {
            width: 100%;
            height: auto;
            margin-bottom: 20px;
        }

            .table th, .table td {
                width: auto;
                height: auto;
                padding: 8px;
                line-height: 20px;
                text-align: left;
                vertical-align: top;
                border-top: 1px solid #dddddd;
            }

            .table th {
                width: auto;
                height: auto;
                font-weight: bold;
            }

            .table thead th {
                vertical-align: bottom;
            }
    </style>

    <h1>Access Codes Used</h1>

    <br />
    <label>Start Date:</label>
    <input type="date" name="start" value="" />
    <label>End Date:</label>
    <input type="date" name="end" value="" />

      <asp:GridView runat="server" ID="datagrid1" UseAccessibleHeader="true" CssClass="table table-bordered table-striped" Width="100%" EmptyDataText="No Load Data!" />
    <br />
    <br />
    <telerik:ReportViewer ID="ReportViewer1" runat="server" Width="100%" ViewMode="Interactive" ScaleMode="FitPageWidth">
        <ReportSource Identifier="C:\Users\leonel.valle\source\repos\Apogee-SN\Portal_Apogee\Report1.cs" IdentifierType="UriReportSource">
        </ReportSource>
    </telerik:ReportViewer>

</asp:Content>
