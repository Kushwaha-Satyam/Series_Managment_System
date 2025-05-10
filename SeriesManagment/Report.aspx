<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="SeriesManagment.Report" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Series Report</title>
    <style>
        body {
            font-family: Arial;
        }

        .report-form {
            width: 60%;
            margin: auto;
            padding: 20px;
            background-color: #f9f9f9;
            border: 1px solid #ccc;
            margin-top: 30px;
            border-radius: 10px;
        }

        .report-form label {
            font-weight: bold;
        }

        .report-form select {
            width: 100%;
            height: 100px;
        }

        .btn {
            margin-top: 10px;
            text-align: center;
        }

        .report-table {
            width: 90%;
            margin: auto;
            margin-top: 30px;
            border-collapse: collapse;
        }

        .report-table th, .report-table td {
            border: 1px solid #ccc;
            padding: 8px;
            text-align: center;
        }

        .report-table th {
            background-color: #f1f1f1;
        }
        #btnGenerate,#btnBack{
            margin-right:10px;
            cursor:pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="report-form">
            <label>Select Year(s):</label><br />
            <asp:ListBox ID="lstYears" runat="server" SelectionMode="Multiple" />
            <div class="btn">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" OnClick="btnGenerate_Click" />
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>

        <asp:Literal ID="ltReportTable" runat="server" />
    </form>
</body>
</html>
