using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLL.DataModel;

namespace DLL.DAL
{
    public class UserModel
    {
        public static bool CreateUser(User user)
        {
            bool check;
            SqlConnection connection = new SqlConnection(BaseClass.Connection);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Users";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            try
            {
                cmd.Parameters.AddWithValue("@userid", user.UserId);
                cmd.Parameters.AddWithValue("@name", user.UserName);
                cmd.Parameters.AddWithValue("@password", user.UserPassword);
                cmd.Parameters.AddWithValue("@groupId", user.GroupId);

                connection.Open();
                check = Convert.ToBoolean(cmd.ExecuteNonQuery());
                connection.Close();
            }
            catch (Exception)
            {
                connection.Dispose();
                throw;
            }
            return check;
        }

        public static DataTable GetUsers()
        {

            SqlConnection connection = new SqlConnection(BaseClass.Connection);

            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GetUsers";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                connection.Open();
                SqlDataAdapter data = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                data.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                connection.Dispose();
                throw;
            }

         
        }


        public static bool LoginUser(string username, string password)
        {
            SqlConnection connection = new SqlConnection(BaseClass.Connection);

            bool check = false;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "LoginUser";
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader reader =cmd.ExecuteReader();

                while (reader.Read())
                {
                    BaseClass.UserName = reader["user_name"].ToString();
                    BaseClass.Permissions.Add(reader["permission_name"].ToString());
                }

                if (BaseClass.Permissions.Count != 0)
                {
                    check = true;
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            return check;
        }

    }
}
