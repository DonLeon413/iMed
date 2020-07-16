using iMed.intrefaces;
using iMedDataSource;
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
using System.Windows.Threading;

namespace iMed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: 
            Window,
            IMessageBoxService // Делаем по простому
    {
        /// <summary>
        /// 
        /// </summary>
        private MainVM _VModel;

        /// <summary>
        /// 
        /// </summary>
        public MainWindow( )
        {
            InitializeComponent();


            this._VModel = new MainVM( this );
            this.DataContext = this._VModel;

            this._VModel.RefreshClients( false );
        }

        #region  interface IMessageBoxService
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="button"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public MessageBoxResult MessageBoxShow( String title, String text,
                                                  MessageBoxButton button = MessageBoxButton.OK,
                                                  MessageBoxImage image = MessageBoxImage.None )
        {
            if( this.Dispatcher.CheckAccess() )
            {
                return MessageBox.Show( this, text, title, button, image );
            }
            else
            {
                return (MessageBoxResult)this.Dispatcher.Invoke( //DispatcherPriority.ApplicationIdle,
                        () => {

                            return MessageBoxShow( title, text, button, image ); 

                        } );
            }
        }

        #endregion
    }
}
