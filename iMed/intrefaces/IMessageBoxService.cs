using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace iMed.intrefaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMessageBoxService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="button"></param>
        /// <param name="image"></param>
        MessageBoxResult MessageBoxShow( String title, String text,
                                         MessageBoxButton button = MessageBoxButton.OK,
                                         MessageBoxImage image = MessageBoxImage.None );
    }
}
