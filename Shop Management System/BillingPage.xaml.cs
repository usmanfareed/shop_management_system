using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DLL.DataModel;
using DLL.DAL;

namespace Shop_Management_System
{
    /// <summary>
    /// Interaction logic for BillingPage.xaml
    /// </summary>
    public partial class BillingPage : Page
    {
        public double TotalPrice { get; set; }
        public double TotalQuantity { get; set; }
        List<Product> list = new List<Product>();

        public BillingPage()
        {
            InitializeComponent();
        }

        private void SearchBoxBil_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Product> data = ProductModel.SearchData(SearchBoxBil.Text);


            var products = data.Select(x => new
            {
                x.Name,
                x.Price,
                image = new BitmapImage(new Uri(Extensions.GetDestinationPath(x.ImagePath, @"ProductImages")))
            });


            dataGridSelect.ItemsSource = products;
        }

        private void DataGridSelect_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product product = new Product();

            //var selected =dataGridSelect.SelectedItem;
            var selected = dataGridSelect.SelectedItems;
            dynamic item = selected[0];
            product.Name = item.Name;
            product.Price = item.Price;
            TotalPrice += item.Price;
            product.Quantity = 1;
            TotalQuantity += product.Quantity;

            list.Add(product);

            //list.Add(selected);

            SubTotalLabel.Content = "Sub Total : " + TotalPrice;
            TotalLabel.Content = "Total : Rs " + TotalPrice;



            dataGridItems.ItemsSource = list;
            //dataGridItems.Items.Add(list);
            dataGridItems.Items.Refresh();
        }


       
        private void dataGridItems_CurrentCellChanged(object sender, EventArgs e)
        {

            List<Product> list = dataGridItems.Items.SourceCollection as List<Product>;
            TotalPrice = 0;
            TotalQuantity = 0;
            foreach (var product in list)
            {


                TotalPrice += product.Price * product.Quantity;
                TotalQuantity += product.Quantity;
            }


            SubTotalLabel.Content = "Sub Total : " + TotalPrice;
            TotalLabel.Content = "Total : Rs " + TotalPrice;

        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            list.Clear();
            dataGridItems.Items.Refresh();

        }

        private void CheckOut_OnClick(object sender, RoutedEventArgs e)
        {
           SalesModel.SaveSalesReport(TotalQuantity);
        }



    }
}
