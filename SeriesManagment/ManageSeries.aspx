<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSeries.aspx.cs" Inherits="SeriesManagment.ManageSeries" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Series</title>

    <!-- DataTables CSS + JS -->
    <link href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#seriesTable').DataTable();
        });
    </script>

    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: #f2f2f2;
        }

        .form-section {
            width: 92%;
            margin: 30px auto;
            border: 1px solid #ccc;
            padding: 30px 40px;
            border-radius: 10px;
            background-color: #ffffff;
            box-shadow: 0px 0px 10px rgba(0,0,0,0.05);
        }

        .form-section h2 {
            text-align: center;
            margin-bottom: 30px;
            color: #333;
        }

        .form-table {
            width: 100%;
            border-collapse: separate;
            border-spacing: 20px 15px;
        }

        .form-table td {
            vertical-align: top;
        }

        .form-table input[type="text"],
        .form-table select {
            width: 100%;
            padding: 8px 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 14px;
        }

        .button-row {
            text-align: center;
            margin-top: 30px;
        }

        .button-row input,
        #btnReport {
            padding: 10px 18px;
            margin: 6px 8px;
            font-size: 15px;
            border: none;
            color: white;
            background-color: forestgreen;
            border-radius: 5px;
            cursor: pointer;
        }

        .button-row input:hover,
        #btnReport:hover {
            background-color: darkgreen;
        }

        #lblMessage {
            padding-top: 10px;
        }

        #btnReport {
            float: right;
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Message Label -->
        <div style="text-align: center;">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Font-Bold="True" />
        </div>

        <!-- Main Section -->
        <div class="form-section">
            <h2>Search Series</h2>

            <!-- Report Button -->
            <asp:Button ID="btnReport" runat="server" Text="📊 View Report" OnClick="btnReport_Click" />

            <!-- Search Fields -->
            <table class="form-table">
                <tr>
                    <td><label>Series API ID:</label></td>
                    <td><asp:TextBox ID="txtApiId" runat="server" /></td>

                    <td><label>Series Name:</label></td>
                    <td><asp:TextBox ID="txtName" runat="server" /></td>
                </tr>
                <tr>
                    <td><label>Series Type:</label></td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Text="-- All --" Value="0" />
                            <asp:ListItem Text="International" Value="1" />
                            <asp:ListItem Text="Domestic" Value="2" />
                            <asp:ListItem Text="Women" Value="3" />
                        </asp:DropDownList>
                    </td>

                    <td><label>Start Date:</label></td>
                    <td><asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" /></td>
                </tr>
                <tr>
                    <td><label>End Date:</label></td>
                    <td><asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" /></td>

                    <td><label>Sort By:</label></td>
                    <td>
                        <asp:DropDownList ID="ddlSortBy" runat="server">
                            <asp:ListItem Text="-- None --" Value="0" />
                            <asp:ListItem Text="Start Date Asc" Value="startasc" />
                            <asp:ListItem Text="Start Date Desc" Value="startdesc" />
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>

            <!-- Action Buttons -->
            <div class="button-row">
                <asp:Button ID="btnAdd" runat="server" Text="➕ Add Series" OnClick="btnAdd_Click" />
                <asp:Button ID="btnSearch" runat="server" Text="🔍 Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnRefresh" runat="server" Text="🔄 Refresh" OnClick="btnRefresh_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="✖ Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <!-- Result Table -->
        <div style="width: 92%; margin: 20px auto;">
            <asp:Literal ID="ltTable" runat="server" />
        </div>
    </form>
</body>
</html>
