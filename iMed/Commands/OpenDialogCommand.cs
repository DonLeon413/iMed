using iMedShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace iMed.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenDialogCommand:
            ICommand
    {
        private Func<Object, Boolean> _CanExecute;
        private Func<Object, Object> _DataLoader;
        private Action<Boolean?, Object> _OnResult;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canExecute"></param>
        /// <param name="dataLoader"></param>
        /// <param name="onResult"></param>
        public OpenDialogCommand( Func<Object, Boolean> canExecute = null,
                                  Func<Object, Object> dataLoader = null,
                                  Action<Boolean?, Object> onResult = null )
        {
            this._CanExecute = canExecute;
            this._DataLoader = dataLoader;
            this._OnResult = onResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute( object parameter )
        {
            TypeInfo p = (TypeInfo)parameter;

            return ( p.BaseType == typeof( DialogBase ) && 
                   ( this._CanExecute == null ? true : this._CanExecute( parameter ) ) );
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute( object parameter )
        {
            if( parameter == null )
                throw new ArgumentNullException( "TargetWindowType" );

            
            TypeInfo p = (TypeInfo)parameter;
            Type t = p.BaseType;

            if( p.BaseType == typeof( DialogBase ) )
            {
                Object data = null;
                try
                {
                    data = ( this._DataLoader == null ? null : this._DataLoader( parameter ) );
                 
                    if( null != data )
                    {
                        var wnd = Activator.CreateInstance( p, new Object[] { data } ) as Window;

                        var result = wnd.ShowDialog();
                        if( null != this._OnResult )
                        {
                            this._OnResult( result, data );
                        }
                    }
                }
                catch( Exception ex )
                { // по простому вывкдкм ошибку
                    MessageBox.Show( ex.Message, "Error", 
                                     MessageBoxButton.OK, MessageBoxImage.Error );    
                }                
            }
        }
    }
}
