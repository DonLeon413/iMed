using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iMed
{
    /// <summary>
    /// Interaction logic for ClientControl.xaml
    /// </summary>
    public partial class ClientControl: UserControl
    {
        private Int32 Id
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ClientControl( Int32 id )
        {
            this.Id = id;

            InitializeComponent();
        }
    }
}
