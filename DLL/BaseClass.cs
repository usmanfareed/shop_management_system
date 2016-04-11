using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLL.DataModel;

namespace DLL
{
    public class BaseClass
    {
        public static string UserName { get; set; }

        public static List<string> Permissions = new List<string>();

        public static List<Product> products = new List<Product>();

        public static double TotalPrice { get; set; }
        public static string Connection
        {
            get { return ConfigurationManager.ConnectionStrings["SMS_DB_Connection"].ConnectionString.ToString(); }
        }



        public void PrintReceipt()
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDialog.Document = printDocument;

            printDocument.PrintPage += PrintDocument_PrintPage;


            DialogResult result = printDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                printDocument.Print();
            }
        }


        



        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphic = e.Graphics;
            Font font = new Font("Courier New",12);

            float fontHeight = font.GetHeight();

            int startX = 10;
            int startY = 10;
            int offset = 40;

            graphic.DrawString("Welcome to Store", new Font("Courier New", 18),new SolidBrush(Color.Black),startX,startY);

            foreach (var product in products)
            {
                string productName = product.Name.PadRight(30);
                string productTotal = string.Format("{0:c}", product.Price);
                string productLine = productName + productTotal;

                graphic.DrawString(productLine,font,new SolidBrush(Color.Black),startX,startY + offset );

                offset = offset + (int) fontHeight + 5;


            }

            offset = offset + 50;
            graphic.DrawString("Total to pay".PadRight(30) + string.Format("{0:c}",TotalPrice),font, new SolidBrush(Color.Black),startX,startY );

        }
    }
}
