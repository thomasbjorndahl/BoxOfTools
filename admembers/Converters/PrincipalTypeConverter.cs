using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ADMembers.Converters
{
    /// <summary>
    /// TODO: WRITE SUMMARY FOR PrincipalTypeConverter
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class PrincipalTypeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch((string)value)
            {
                case "user":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/User32.png"));
                case "computer":
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Computer32.png"));
                default:
                    return new BitmapImage(new Uri("pack://application:,,,/Images/Group32.png"));
            }
           
            //switch ((Int32)value)
            //{

            //    case ((int)WorkflowStatuses.CreateException): return new BitmapImage(new Uri("pack://application:,,,/Images/Pizza025.png"));
            //    case ((int)WorkflowStatuses.ReviewException): return new BitmapImage(new Uri("pack://application:,,,/Images/Pizza050.png"));
            //    case ((int)WorkflowStatuses.ApproveException): return new BitmapImage(new Uri("pack://application:,,,/Images/Pizza075.png"));
            //    case ((int)WorkflowStatuses.WorkflowApproved): return new BitmapImage(new Uri("pack://application:,,,/Images/PizzaGreen.png"));
            //    case ((int)WorkflowStatuses.WorkflowRejected): return new BitmapImage(new Uri("pack://application:,,,/Images/PizzaRed.png"));
            //    default:
            //    case -1: return new BitmapImage(new Uri("pack://application:,,,/Images/Pizza000.png"));
            //}
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
