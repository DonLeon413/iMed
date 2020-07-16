using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace iMedShared
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogBase: Window
    {
        /// <summary>
        /// 
        /// </summary>
        public DialogBase()
        {            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public void SetDialogResult( Boolean result)
        {
            this.DialogResult = result;
        }
    }
}
