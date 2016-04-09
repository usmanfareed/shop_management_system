using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shop_Management_System
{
    public static class Extensions
    {
        public static ImageSource FromStringToImage(string image)
        {
            byte[] imageBytes = Convert.FromBase64String(image);
            BitmapImage convert = new BitmapImage();
            convert.BeginInit();
            convert.StreamSource = new MemoryStream(imageBytes);
            convert.EndInit();
            return convert;
        }

        public static string FromImageToString(object image)
        {
            try
            {
                return Convert.ToBase64String(File.ReadAllBytes(image.ToString()));

            }
            catch 
            {

                ImageConverter converter = new ImageConverter();
                return Convert.ToBase64String((byte[])converter.ConvertTo(image as Image, typeof(byte[])));
            }

        }


        public static String GetDestinationPath(string filename, string foldername)
        {
            String appStartPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            appStartPath = String.Format(appStartPath +  @"\..\..\..\DLL\{0}\" + filename, foldername);
            return appStartPath;
        }

    }
}