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
using DLL.DAL;
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


           bool check = UserModel.LoginUser(username.Text.Trim(), password.Password.Trim());


           

            if (check)
            {


                MainWindow window = new MainWindow();
                window.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Wrong User Name or Password");
            }


            

        }

     
    }
}
