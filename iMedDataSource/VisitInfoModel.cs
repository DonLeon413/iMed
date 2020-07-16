using iMedDataSource.enums;
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
    public class VisitInfoModel:
            DialogVMBase
    {
        private Action<VisitInfoModel> _SaveFunc = null;
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
        public Int32 ClientId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        private DateTime _DataVisit = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public DateTime DateVisit
        {
            get
            {
                return this._DataVisit;
            }

            set 
            {
                if( this._DataVisit != value )
                {
                    this._DataVisit = value;
                    OnPropertyChanged( "DateVisit" );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private eVisitType _VisitType = eVisitType.Unknown;

        /// <summary>
        /// 
        /// </summary>
        public eVisitType VisitType
        {
            get 
            {
                return this._VisitType;
            }

            set
            {
                if( this._VisitType != value )
                {
                    this._VisitType = value;
                    OnPropertyChanged( "VisitType" );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private String _Diagnos;

        /// <summary>
        /// 
        /// </summary>
        public String Diagnos
        {
            get
            {
                return this._Diagnos;
            }

            set 
            {
                if( 0 != String.Compare( this._Diagnos, value, true ) )
                {
                    this._Diagnos = value;
                    OnPropertyChanged( "Diagnos" );
                }
            }
        }

        /// <summary>
        /// Ctor default
        /// </summary>
        /// <param name="saveFunction"></param>
        public VisitInfoModel( Action<VisitInfoModel> saveFunction )
        {
            this._DataVisit = DateTime.Now;

            this._SaveFunc = saveFunction;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnSuccessSave( )
        {
            this.SuccessSave( this, new EventArgs() );
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnErrorSave( )
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
