using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DLL.DataModel;
using DLL;
using MahApps.Metro.Controls;

namespace Shop_Management_System
{
    /// <summary>
    /// Interaction logic for LoginWin.xaml
    /// </summary>
    public partial class LoginWin : MetroWindow
    {
        public LoginWin()
        {
            InitializeComponent();
            username.Focus();
        }

        
        private void Login_btn(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(BaseClass.Connection);
            
            string query = "SELECT * FROM um_users WHERE user_name='" + username.Text.Trim() + "' AND password='" + password.Text.Trim() +
                           "' ";

            con.Open();

            SqlCommand selectCommand = new SqlCommand(query, con);

            SqlDataReader reader = selectCommand.ExecuteReader();


            reader.Read();

            if (!String.IsNullOrWhiteSpace(reader["user_name"].ToString()) && !String.IsNullOrWhiteSpace(reader["password"].ToString()))
            {


                BaseClass.UserId = Convert.ToInt32(reader["userID"]);
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
                con.Close();
                con.Dispose();
            }




        }

     
    }
}
