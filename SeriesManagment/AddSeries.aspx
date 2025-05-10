<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSeries.aspx.cs" Inherits="SeriesManagment.AddSeries" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add / Update Series</title>
    <style>
    body {
        font-family: 'Segoe UI', sans-serif;
    }

    .form-container {
        width: 95%;
        margin: 30px auto;
    }

    .form-heading {
        background-color: #d9f0ff;
        padding: 10px 20px;
        font-weight: bold;
        font-size: 16px;
        color: #e60000;
        border-top-left-radius: 10px;
        border-top-right-radius: 10px;
    }

    .form-box {
        background-color: #d4d1c7;
        padding: 30px;
        border-bottom-left-radius: 10px;
        border-bottom-right-radius: 10px;
    }

    .form-grid {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 20px 30px;
    }

    .form-group {
        display: flex;
        flex-direction: column;
    }

    .input {
        padding: 8px 10px;
        border: 1px solid #ccc;
        border-radius: 5px;
        font-size: 14px;
    }

    .small-button {
        padding: 5px 10px;
        font-size: 16px;
        background-color: #28a745;
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

    .note {
        font-size: 12px;
        color: red;
        margin-top: 5px;
    }

    .full {
        grid-column: span 3;
    }

    .button-group {
        text-align: center;
        margin-top: 30px;
    }

    .form-button {
        background-color: green;
        color: white;
        font-size: 16px;
        padding: 10px 20px;
        margin: 0 10px;
        border: none;
        border-radius: 5px;
        cursor: pointer;
    }

    .form-button:hover {
        background-color: darkgreen;
    }
</style>

</head>
<body style="background-color: #f0f0f0;">
    <form id="form1" runat="server">
        <div class="form-container">
            <div class="form-heading">
                <span>Series detail</span>
            </div>

            <div class="form-box">
                <div class="form-grid">
                    <!-- ROW 1 -->
                    <div class="form-group">
                        <label>Series Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="input" placeholder="Series Name" />
                    </div>
                    <div class="form-group">
                        <label>Series Type</label>
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="input" >
                          <asp:ListItem Text="-- Select --" Value="0" />
                        <asp:ListItem Text="International" Value="1" />
                        <asp:ListItem Text="Domestic" Value="2" />
                        <asp:ListItem Text="Women" Value="3" />
                        <asp:ListItem Text="Mens" Value="4" />
                         </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Series Status</label>
                        <asp:DropDownList ID="ddlSeriesStatus" runat="server" CssClass="input">
                            <asp:ListItem Text="-- Select --" Value="" />
                        <asp:ListItem Text="Scheduled" Value="Scheduled" />
                        <asp:ListItem Text="Completed" Value="Completed" />
                        <asp:ListItem Text="Live" Value="Live" />
                        <asp:ListItem Text="Abandon" Value="Abandon" />
                        </asp:DropDownList>
                    </div>

                    <!-- ROW 2 -->
                    <div class="form-group">
                        <label>Match Status</label>
                        <asp:DropDownList ID="ddlMatchStatus" runat="server" CssClass="input">
                        <asp:ListItem Text="-- Select --" Value="" />
                        <asp:ListItem Text="Scheduled" Value="Scheduled" />
                        <asp:ListItem Text="Completed" Value="Completed" />
                        <asp:ListItem Text="Live" Value="Live" />
                        <asp:ListItem Text="Abandon" Value="Abandon" />
                    </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Match Format</label>
                         <asp:DropDownList ID="ddlMatchFormat" runat="server" CssClass="input">
                        <asp:ListItem Text="-- Select --" Value="" />
                        <asp:ListItem Text="ODI" Value="ODI" />
                        <asp:ListItem Text="TEST" Value="TEST" />
                        <asp:ListItem Text="T20" Value="T20" />
                        <asp:ListItem Text="T10" Value="T10" />
                    </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Series Match Type</label>
                        <asp:DropDownList ID="ddlMatchType" runat="server" CssClass="input">
                        <asp:ListItem Text="-- Select --" Value="0" />
                        <asp:ListItem Text="ODI" Value="1" />
                        <asp:ListItem Text="TEST" Value="2" />
                        <asp:ListItem Text="T20I" Value="3" />
                        <asp:ListItem Text="LIST A" Value="4" />
                        <asp:ListItem Text="T20 (Domestic)" Value="5" />
                        <asp:ListItem Text="First Class" Value="6" />
                    </asp:DropDownList>
                    </div>

                    <!-- ROW 3 -->
                    <div class="form-group">
                        <label>Gender</label>
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="input">
                        <asp:ListItem Text="-- Select --" Value="0" />
                        <asp:ListItem Text="Mens" Value="1" />
                        <asp:ListItem Text="Womens" Value="2" />
                        <asp:ListItem Text="Other" Value="3" />
                    </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Year</label>
                        <asp:TextBox ID="txtYear" runat="server" CssClass="input" placeholder="Year" />
                    </div>
                      <div class="form-group">
                    <label>Trophy Type:</label>
                    <asp:DropDownList ID="ddlTrophyType" runat="server" CssClass="input">
                        <asp:ListItem Text="-- Select --" Value="0" />
                        <asp:ListItem Text="Asia Cup" Value="1" />
                        <asp:ListItem Text="ICC World Cup T20" Value="2" />
                    </asp:DropDownList>
                </div>

                    <!-- ROW 4 -->
                    <div class="form-group">
                        <label>Series Start Date</label>
                        <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" CssClass="input" />
                        <small class="note">Note : All dates and times scheduled in GMT.</small>
                    </div>
                    <div class="form-group">
                        <label>Series End Date</label>
                        <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" CssClass="input" />
                    </div>
                    <div class="form-group">
                        <label>Is Active</label>
                         <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="input">
                        <asp:ListItem Text="Yes" Value="1" />
                        <asp:ListItem Text="No" Value="0" />
                    </asp:DropDownList>
                    </div>

                    <!-- ROW 5 -->
                    <div class="form-group full">
                        <label>Description</label>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="input" />
                    </div>
                </div>

                <!-- BUTTONS -->
                <div class="button-group">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-button" OnClick="btnSave_Click" />
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="form-button" OnClick="btnRefresh_Click" CausesValidation="False" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="form-button" OnClick="btnCancel_Click" CausesValidation="False" />
                </div>

                <div style="text-align:center; margin-top:10px;">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
                </div>
            </div>
        </div>
    </form>
</body>

</html>
