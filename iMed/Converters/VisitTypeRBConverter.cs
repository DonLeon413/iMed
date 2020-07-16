using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace iMed
{
    /// <summary>
    /// 
    /// </summary>
    public class VisitTypeRBConverter: 
                 IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert( object value, Type targetType, object parameter,
                               System.Globalization.CultureInfo culture )
        {
            return value.Equals( parameter );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack( object value, Type targetType, object parameter,
                                   System.Globalization.CultureInfo culture )
        {
            return value.Equals( true ) ? parameter : Binding.DoNothing;
        }
    }

}
