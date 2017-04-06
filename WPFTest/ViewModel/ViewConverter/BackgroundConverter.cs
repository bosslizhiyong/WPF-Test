#region Copyright
/*-----------------------------------------------------------------
 * 文件名（File Name）：          BackgroundConverter.cs
 *
 * 作  者（Author）：             李志勇（John）
 *
 * 日  期（Create Date）：        2017-03-27 17:32:23
 *
 * 公  司（Copyright）：          www.Leapline.com.cn
 * ----------------------------------------------------------------
 * 描  述（Description）：
 * 
 * ----------------------------------------------------------------
 * 修改记录（Revision History）
 *      R1 -
 *         修改人（Editor）：
 *         修改日期（Date）：
 *         修改原因（Reason）：
 *----------------------------------------------------------------*/
#endregion Copyright

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
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
  
}
