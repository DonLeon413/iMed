using iMedShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMedDataSource
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientInfoModel:
                 DialogVMBase
    {
        private Action<ClientInfoModel> _SaveFunc = null;
        /// <summary>
        /// 
        /// </summary>
        public EventHandler<EventArgs> SuccessSave = ( o, e ) => { };

        /// <summary>
        /// 
        /// </summary>
        public EventHandler<EventArgs> ErrorSave = ( o, e ) => { };

        /// <summary>
        /// 
        /// </summary>
        public Int32 Id
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public String SecondName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public String ThirdName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime BirthDay
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public String Address
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ClientInfoModel( Action<ClientInfoModel> saveFunction )
        {
            this.BirthDay = DateTime.Now;

            this._SaveFunc = saveFunction; 
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnSuccessSave()
        {
            this.SuccessSave( this, new EventArgs() );
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnErrorSave()
        {
            this.ErrorSave( this, new EventArgs() );
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SaveData( )
        {
            if( null != this._SaveFunc )
            {
                this._SaveFunc( this );
            }
        }
    }
}
