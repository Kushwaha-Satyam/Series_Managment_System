using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SeriesManagment.BAL;
using System.Configuration;
using System.Data;

namespace SeriesManagment.DAL
{
    public class SeriesDAL
    {
        string CS = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        public int InsertSeries(SeriesBAL obj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("prcTblSeriesInsert", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SeriesName", obj.SeriesName);
                    cmd.Parameters.AddWithValue("@SeriesType", obj.SeriesType);
                    cmd.Parameters.AddWithValue("@SeriesStatus", obj.SeriesStatus);
                    cmd.Parameters.AddWithValue("@MatchStatus", obj.MatchStatus);
                    cmd.Parameters.AddWithValue("@MatchFormat", obj.MatchFormat);
                    cmd.Parameters.AddWithValue("@SeriesMatchType", obj.SeriesMatchType);
                    cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@Year", obj.Year);
                    cmd.Parameters.AddWithValue("@TrophyType", obj.TrophyType);
                    cmd.Parameters.AddWithValue("@SeriesStartDate", obj.SeriesStartDate);
                    cmd.Parameters.AddWithValue("@SeriesEndDate", obj.SeriesEndDate);
                    cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogError("InsertSeries", ex.Message);
                return 0;
            }
        }
        //Delete Series
        public void DeleteSeries(int seriesId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM tbl_Series WHERE SeriesId = @SeriesId", conn);
                    cmd.Parameters.AddWithValue("@SeriesId", seriesId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogError("DeleteSeries", ex.Message);
            }
        }

        public int UpdateSeries(SeriesBAL obj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("prcTblSeriesUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SeriesId", obj.SeriesId);
                    cmd.Parameters.AddWithValue("@SeriesName", obj.SeriesName);
                    cmd.Parameters.AddWithValue("@SeriesType", obj.SeriesType);
                    cmd.Parameters.AddWithValue("@SeriesStatus", obj.SeriesStatus);
                    cmd.Parameters.AddWithValue("@MatchStatus", obj.MatchStatus);
                    cmd.Parameters.AddWithValue("@MatchFormat", obj.MatchFormat);
                    cmd.Parameters.AddWithValue("@SeriesMatchType", obj.SeriesMatchType);
                    cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@Year", obj.Year);
                    cmd.Parameters.AddWithValue("@TrophyType", obj.TrophyType);
                    cmd.Parameters.AddWithValue("@SeriesStartDate", obj.SeriesStartDate);
                    cmd.Parameters.AddWithValue("@SeriesEndDate", obj.SeriesEndDate);
                    cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogError("UpdateSeries", ex.Message);
                return 0;
            }
        }

        public DataTable SearchSeries(int? apiId, string name, int? type, DateTime? start, DateTime? end)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("prcTblSeriesSearch", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SeriesApiId", (object)apiId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SeriesName", (object)name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SeriesType", (object)type ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@StartDate", (object)start ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", (object)end ?? DBNull.Value);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                LogError("SearchSeries", ex.Message);
            }
            return dt;
        }

        private void LogError(string func, string msg)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO ErrorLog (ErrorMessage, PageName, FunctionName) VALUES (@msg, @page, @func)", conn);
                cmd.Parameters.AddWithValue("@msg", msg);
                cmd.Parameters.AddWithValue("@page", "SeriesDAL.cs");
                cmd.Parameters.AddWithValue("@func", func);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
