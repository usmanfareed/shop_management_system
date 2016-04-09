using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DLL.DataModel;

namespace DLL.DAL
{
    public class ProductModel
    {
        public static string InsertProduct(Product product)
        {
            try
            {
                SqlConnection connection = new SqlConnection(BaseClass.Connection);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InsertUpdateProduct";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ProductID", product.Id);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@imagePath", product.ImagePath);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@AddedDate", product.AddedDate);
                cmd.Parameters.AddWithValue("@AddedBy", product.Addedby);
                cmd.Parameters.AddWithValue("@UpdatedDate", product.UpdatedDate);
                cmd.Parameters.AddWithValue("@UpdatedBy", product.UpdatedBy);
                cmd.Parameters.AddWithValue("@isAvailable", product.IsAvailable);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return "Success";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Product> GetProducts()
        {
            try
            {
                SqlConnection connection = new SqlConnection(BaseClass.Connection);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GetProducts";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                List<Product> list = new List<Product>();

                while (reader.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(reader["productID"]);
                    product.Name = reader["name"].ToString();
                    product.Price = Convert.ToInt32(reader["price"]);
                    product.Quantity = Convert.ToInt32(reader["quantity"]);
                    product.Description = reader["description"].ToString();
                    product.ImagePath = reader["imagePath"].ToString();
                    list.Add(product);
                }

                connection.Close();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteSelected(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    SqlConnection connection = new SqlConnection(BaseClass.Connection);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "DeleteProduct";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static List<Product> SearchData(string keyword)
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(BaseClass.Connection);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SearchProduct";
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@keyword", keyword);
            connection.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            List<Product> list = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(reader["productID"]);
                product.Name = reader["name"].ToString();
                product.Price = Convert.ToInt32(reader["price"]);
                product.Quantity = Convert.ToInt32(reader["quantity"]);
                product.Description = reader["description"].ToString();
                product.ImagePath = reader["imagePath"].ToString();
                list.Add(product);
            }

            connection.Close();
            return list;
        }


    }
}
