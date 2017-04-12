
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFTest
{
    public class BackgroundConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color color = new Color();
           // int num = int.Parse(value.ToString());
            string status = value.ToString();
            if (status == "失败")
                color = Colors.Yellow;
            else if (status == "运行")
                color = Colors.LightGreen;
            else
                color = Colors.LightPink;
            return new SolidColorBrush(color);


           //测试
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
  
}
