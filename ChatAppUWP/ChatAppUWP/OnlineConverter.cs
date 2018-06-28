using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace ChatAppUWP
{
    class OnlineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                string url = "";
                if ((bool)value)
                    url = @"ms-appx:///Assets/" + "online.png";
                else
                    url = @"ms-appx:///Assets/" + "offline.png";

                return new BitmapImage(new Uri(url));
            }
            catch
            {
                return new BitmapImage(new Uri(@"ms-appx:///Assets/" + "offline.png", UriKind.RelativeOrAbsolute)); ;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
