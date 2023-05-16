<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manto.aspx.cs" Inherits="Portal_Apogee.Manto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">






    <style type="text/css">
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

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete this Access Code?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
            window.onload() = Confirm;
        }
    </script>
    <h1>Delete Top Assy Records (Plant 3)</h1>
    <br />
    <br />
    <label>Search: </label>
    <input type="text" name="txt_ac" id="txt_ac" runat="server" />
    <asp:Button ID="btn_Search" runat="server" Text="Search" OnClick="btn_Search_Click" />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound1">
        <Columns>

            <asp:CommandField SelectText="Delete" ShowSelectButton="True" HeaderText="Action" />
        </Columns>
        <SelectedRowStyle BackColor="Yellow" />
    </asp:GridView>




</asp:Content>
