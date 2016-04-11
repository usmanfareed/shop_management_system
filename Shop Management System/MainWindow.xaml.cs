using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using DLL;
using MahApps.Metro.Controls;

namespace Shop_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        



        dynamic billingPage = new BillingPage();
        dynamic inventoryPage = new InventoryPage();
        dynamic ReportPage = new report();
        dynamic UsersPage = new UsersPage();
        

        public MainWindow()
        {
            InitializeComponent();
            
            frame.NavigationService.Navigate(billingPage);


            foreach (var permission in BaseClass.Permissions)
            {

                switch (permission)
                {
                    case "BillingSection":
                    {
                        btn_Billing.Visibility = Visibility.Visible;
                        lbl_Billing.Visibility = Visibility.Visible;
                    }
                        break;

                    case "InventorySection":
                    {
                            btn_Inventory.Visibility = Visibility.Visible;
                            lbl_Inventory.Visibility = Visibility.Visible;
                        }
                        break;

                    case "ReportSection":
                    {
                            btn_Reports.Visibility = Visibility.Visible;
                            lbl_Reports.Visibility = Visibility.Visible;
                        }
                        break;

                    case "UserSection":
                    {
                            btn_Users.Visibility = Visibility.Visible;
                            lbl_Users.Visibility = Visibility.Visible;
                        }
                        break;
                }

            }

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(billingPage);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(inventoryPage);
        }

        private void Button_Copy1_OnClick(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(ReportPage);

        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(UsersPage);

        }
    }
}
