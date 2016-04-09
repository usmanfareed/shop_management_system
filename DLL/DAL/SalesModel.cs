using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.DataModel;

namespace DLL.DAL
{
    public class SalesModel
    {
        public static void SaveSalesReport(double totalQuantity)
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(BaseClass.Connection);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SaveSalesReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@TotalQuantity", totalQuantity);
            cmd.Parameters.AddWithValue("@Date", DateTime.Today.Date);

            connection.Open();
            cmd.ExecuteNonQuery();

            connection.Close();
        }


        public static Dictionary<string, int> GetSalesReport(DateTime fromDate, DateTime toDate)
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(BaseClass.Connection);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetSalesReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@fromDate", fromDate);
            cmd.Parameters.AddWithValue("@toDate", toDate);

            connection.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            Dictionary<string, int> data = new Dictionary<string, int>();

            while (reader.Read())
            {
                int Quantity = Convert.ToInt32(reader["sold_product_quantity"]);
                DateTime date = Convert.ToDateTime(reader["date"]);
                data.Add(date.ToShortDateString().ToString(), Quantity);
            }

            connection.Close();
            return data;
        }

        public static Dictionary<string, int> GetSalesYearlyReport(DateTime fromDate, DateTime toDate)
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(BaseClass.Connection);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetSalesReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@fromDate", fromDate);
            cmd.Parameters.AddWithValue("@toDate", toDate);

            connection.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            Dictionary<DateTime, int> data = new Dictionary<DateTime, int>();
            Dictionary<string, int> MonthlyReport = new Dictionary<string, int>();

            while (reader.Read())
            {
                int Quantity = Convert.ToInt32(reader["sold_product_quantity"]);
                DateTime date = Convert.ToDateTime(reader["date"]);
                data.Add(date.Date, Quantity);
            }

            foreach (var record in data)
            {
                switch (record.Key.Month.ToString())
                {
                    case "1":

                        if (!MonthlyReport.ContainsKey("Janurary"))
                        {
                            MonthlyReport.Add("Janurary", 0);
                        }

                        MonthlyReport["Janurary"] += record.Value;

                        break;

                    case "2":

                        if (!MonthlyReport.ContainsKey("February"))
                        {
                            MonthlyReport.Add("February", 0);
                        }

                        MonthlyReport["February"] += record.Value;

                        break;

                    case "3":

                        if (!MonthlyReport.ContainsKey("March"))
                        {
                            MonthlyReport.Add("March", 0);
                        }

                        MonthlyReport["March"] += record.Value;

                        break;

                    case "4":

                        if (!MonthlyReport.ContainsKey("April"))
                        {
                            MonthlyReport.Add("April", 0);
                        }

                        MonthlyReport["April"] += record.Value;

                        break;

                    case "5":

                        if (!MonthlyReport.ContainsKey("May"))
                        {
                            MonthlyReport.Add("May", 0);
                        }

                        MonthlyReport["May"] += record.Value;

                        break;

                    case "6":

                        if (!MonthlyReport.ContainsKey("June"))
                        {
                            MonthlyReport.Add("June", 0);
                        }

                        MonthlyReport["June"] += record.Value;

                        break;

                    case "7":

                        if (!MonthlyReport.ContainsKey("July"))
                        {
                            MonthlyReport.Add("July", 0);
                        }

                        MonthlyReport["July"] += record.Value;

                        break;

                    case "8":

                        if (!MonthlyReport.ContainsKey("August"))
                        {
                            MonthlyReport.Add("August", 0);
                        }

                        MonthlyReport["August"] += record.Value;

                        break;

                    case "9":

                        if (!MonthlyReport.ContainsKey("September"))
                        {
                            MonthlyReport.Add("September", 0);
                        }

                        MonthlyReport["September"] += record.Value;

                        break;

                    case "10":

                        if (!MonthlyReport.ContainsKey("Octuber"))
                        {
                            MonthlyReport.Add("Octuber", 0);
                        }

                        MonthlyReport["Octuber"] += record.Value;

                        break;

                    case "11":

                        if (!MonthlyReport.ContainsKey("November"))
                        {
                            MonthlyReport.Add("November", 0);
                        }

                        MonthlyReport["November"] += record.Value;

                        break;

                    case "12":

                        if (!MonthlyReport.ContainsKey("December"))
                        {
                            MonthlyReport.Add("December", 0);
                        }

                        MonthlyReport["December"] += record.Value;

                        break;
                }
            }

            connection.Close();
            return MonthlyReport;
        }
    }
}
