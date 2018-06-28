using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ChatAppUWP
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                string url = @"ms-appx:///Assets/Photos/" + ((string)value);

                return new BitmapImage(new Uri(url));
            }
            catch
            {
                return new BitmapImage(new Uri(@"ms-appx:///Assets/Photos/" + "60838637_p0_master1200.jpg", UriKind.RelativeOrAbsolute)); ;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
        
    }
}
