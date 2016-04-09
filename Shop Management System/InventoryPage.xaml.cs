using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DLL.DataModel;
using DLL.DAL;
using Microsoft.Win32;

namespace Shop_Management_System
{
    /// <summary>
    /// Interaction logic for InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : Page
    {
        public InventoryPage()
        {
            InitializeComponent();
            loadGrid();
        }

        public string FilePath { get; set; }


        public string FileName { get; set; }
        public int Id { get; set; }

        private void btn_Upload_Click(object sender, RoutedEventArgs e)
        {
            FileDialog dialoag = new OpenFileDialog();
            dialoag.Title = "Select a picture";
            dialoag.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                             "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                             "Portable Network Graphic (*.png)|*.png";


            if (dialoag.ShowDialog() == true)
            {
                FilePath = dialoag.FileName;


                image.Source = new BitmapImage(new Uri(dialoag.FileName));

                //ImageData = Extensions.FromImageToString(dialoag.FileName);
            }

            FileName = System.IO.Path.GetFileName(FilePath);
            string destinationPath = Extensions.GetDestinationPath(FileName, @"ProductImages");


            File.Copy(FilePath, destinationPath,true);
        }


        private void AddProduct_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product();

                product.Id = Id;
                product.Name = productName.Text;
                product.Price = Convert.ToInt32(productPrice.Text);
                product.Quantity = Convert.ToInt32(productQty.Text);
                product.Description = productDes.Text;
                product.ImagePath = FileName;
                product.AddedDate = DateTime.Now;
                product.Addedby = "unknown";
                product.UpdatedDate = DateTime.Now;
                product.UpdatedBy = "unknow";
                product.IsAvailable = Convert.ToByte((Availability.Text == "YES") ? 1 : 0);

                string message = ProductModel.InsertProduct(product);
                MessageBox.Show(message);

                loadGrid();
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void loadGrid()
        {
            List<Product> list = ProductModel.GetProducts();
            dataGrid.ItemsSource = list;
        }

        private void DataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product product = (Product) dataGrid.SelectedItem;

            Id = product.Id;
            productName.Text = product.Name;
            productPrice.Text = product.Price.ToString();
            productQty.Text = product.Quantity.ToString();
            productDes.Text = product.Description;
            Availability.SelectedItem = (product.IsAvailable == 1) ? "TRUE" : "FALSE";
            FileName = product.ImagePath;
            string destinationPath = Extensions.GetDestinationPath(FileName, @"ProductImages");
            image.Source = new BitmapImage(new Uri(destinationPath));
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            Id = 0;
            productName.Text = null;
            productPrice.Text = null;
            productQty.Text = null;
            productDes.Text = null;
            image.Source = null;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            List<int> ids = new List<int>();

            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                var row = (DataGridRow) dataGrid.ItemContainerGenerator.ContainerFromIndex(i);

                Product product = row.Item as Product;
                CheckBox checkBox = dataGrid.Columns[0].GetCellContent(row) as CheckBox;
                if (checkBox.IsChecked.Value)
                {
                    ids.Add(product.Id);
                }
            }

            ProductModel.DeleteSelected(ids);
            loadGrid();
        }

        private void SearchBoxInv_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Product> products = ProductModel.SearchData(SearchBoxInv.Text);


            dataGrid.ItemsSource = products;
        }
    }
}
