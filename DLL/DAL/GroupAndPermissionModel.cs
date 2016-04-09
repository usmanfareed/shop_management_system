using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLL.DataModel;

namespace DLL.DAL
{
    public class GroupAndPermissionModel
    {
        public static string InsertProduct(int groupId, string groupName, List<int> permissions)
        {
            string flag = "true";
            foreach (var permission in permissions)
            {
                if (permission != 0)
                {
                    try
                    {
                        SqlConnection connection = new SqlConnection(BaseClass.Connection);
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "GroupsAndPermissons";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@groupId", groupId);
                        cmd.Parameters.AddWithValue("@groupName", groupName);
                        cmd.Parameters.AddWithValue("@permission", permission);
                        cmd.Parameters.AddWithValue("@flag", flag);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        flag = "false";

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return "Success";
        }


        public static List<Group> GetGroups()
        {
            try
            {
                SqlConnection connection = new SqlConnection(BaseClass.Connection);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from user_group order by groupID";
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Group> list = new List<Group>();

                while (reader.Read())
                {
                    Group group = new Group();

                    group.GroupId = Convert.ToInt32(reader["groupID"]);
                    group.GroupName= reader["group_name"].ToString();
                  
                    list.Add(group);
                }

                connection.Close();
                return list;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static List<Group> GetGroupsPermission()
        {
            try
            {
                SqlConnection connection = new SqlConnection(BaseClass.Connection);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GetGroups";
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Group> list = new List<Group>();

                while (reader.Read())
                {
                    Group group = new Group();

                    group.GroupId = Convert.ToInt32(reader["groupID"]);
                    group.GroupName = reader["group_name"].ToString();
                    group.PermissionName = reader["permission_name"].ToString();


                    list.Add(group);
                }

                connection.Close();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }  



    }


    
}
