using iMedShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace iMedShared
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogVMBase:
                 NotifyModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual void SaveData( )
        { 
        }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual void OnSuccessSave()
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnErrorSave( )
        {
        }

        private String _MessageInfo;

        /// <summary>
        /// 
        /// </summary>
        public String MessageInfo 
        {
            get
            {
                return this._MessageInfo;
            }
            set
            {
                if( 0 != String.Compare( this._MessageInfo, value ) )
                {
                    this._MessageInfo = value;
                    OnPropertyChanged( "MessageInfo" );
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public DialogVMBase()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Boolean SaveModel()
        {
            Boolean result = true;

            try
            {
                this.SaveData();
                this.MessageInfo = "";
            }
            catch( Exception ex )
            {
                this.MessageInfo = ex.Message;

                result = false;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private RelayCommand _OKCmd;

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand OKCmd
        {
            get
            {
                return this._OKCmd ??
                    ( this._OKCmd = new RelayCommand( 
                        ( obj ) => 
                        { 
                            var result = SaveModel();
                            if( result )
                            {                                
                                var dialog_base = (DialogBase)obj;
                                if( null != dialog_base )
                                {
                                    dialog_base.SetDialogResult( true );
                                }
                                OnSuccessSave();
                            }
                            else
                            {
                                OnErrorSave();
                            }
                        },
                    ( obj ) => 
                    { 
                        return true; 
                    } ) );
            }
        }

    }
}
