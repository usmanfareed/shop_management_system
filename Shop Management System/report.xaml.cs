using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DLL.DataModel;
using DLL.DAL;

namespace Shop_Management_System
{
    /// <summary>
    /// Interaction logic for report.xaml
    /// </summary>
    public partial class report : Page
    {
        public report()
        {
            InitializeComponent();

            WeeklyReport.ItemsSource= SalesModel.GetSalesReport(DateTime.Today.Date.AddDays(-7), DateTime.Today.Date);
            MonthlyReport.ItemsSource= SalesModel.GetSalesYearlyReport(DateTime.Today.Date.AddYears(-1), DateTime.Today.Date);
        }


        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           var data = SalesModel.GetSalesReport(DatePicker.SelectedDate.Value.Date, DatePicker.SelectedDate.Value.Date);
            if (data.ContainsKey(DatePicker.SelectedDate.Value.Date.ToShortDateString()))
            {
            SpecificReport.Content = data[DatePicker.SelectedDate.Value.Date.ToShortDateString()].ToString();

            }
            else
            {
                SpecificReport.Content = "     No data";
            }

        }
    }
}
