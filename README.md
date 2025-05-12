# 🏏 Series Management System – ASP.NET Web Forms (3-Tier Architecture)

This is a dynamic web-based application to manage cricket series data, allowing users to **add**, **edit**, **search**, **delete**, and generate **dynamic reports** by year, gender, and match format.

---

## 📌 Features

- ✅ Add/Edit Series details
- ✅ Filtered search with sort by date
- ✅ Delete series with confirmation
- ✅ View/Edit via encrypted query string
- ✅ Dynamic report (pivot-style) by year × gender × match format
- ✅ Success messages, validation, and 3-tier architecture
- ✅ DataTables integration for search/sort/pagination

---

## 🏗 Architecture – 3-Tier Design

[ Presentation Layer ] → Web Forms (.aspx)
↓
[ Business Logic Layer ] → BAL Classes
↓
[ Data Access Layer ] → DAL + ADO.NET + Stored Procedures
↓
[ SQL Server Database ]


---

## 💻 Technologies Used

| Layer        | Tech Stack                        |
|--------------|----------------------------------|
| Frontend     | ASP.NET Web Forms (.aspx)         |
| Backend      | C#, ADO.NET                        |
| Database     | SQL Server                        |
| Reporting    | HTML Table (Pivot logic in C#)   |
| UI Styling   | HTML, CSS                         |
| Scripts      | jQuery + DataTables               |
| Security     | Query string encryption (Crypto)  |

---

## 📄 Pages Overview

### 🔹 `AddSeries.aspx`
- 3-column layout
- Fields: Name, Type, Match Format, Year, Gender, Start/End Dates
- Buttons: Save, Refresh, Cancel

### 🔹 `ManageSeries.aspx`
- Search filters: API ID, Name, Type, Dates
- Sort by Start Date
- Table with Edit + Delete links
- View Report button

### 🔹 `Report.aspx`
- Multi-select year dropdown
- Pivot-like table for Match Format vs. (Year × Gender)
- Total row at the bottom

---

## 🗄️ Database Tables

- `tbl_Series`: Stores all series details
- `ErrorLog`: Logs runtime errors

---

## 🧠 Stored Procedures

- `prcTblSeriesInsert`
- `prcTblSeriesUpdate`
- `prcTblSeriesSearch`
- `prcTblSeriesDelete`
- `prc_Report_MatchFormatYearGender`

---

## ⚙️ How to Run

1. Clone/download the repo
2. Open solution in Visual Studio
3. Configure your connection string in `web.config`
4. Restore database (SQL script provided if needed)
5. Run the project (`F5`) and use:
   - `/AddSeries.aspx`
   - `/ManageSeries.aspx`
   - `/Report.aspx`

---

## 🙋‍♂️ Author

**Satyam Kushwaha**  
Fresher – .NET Developer  
[LinkedIn Profile](https://www.linkedin.com/in/satyam-kushwaha-6ba6a8302)

---

## 📃 License

This project is for learning, academic, and demonstration purposes only.
