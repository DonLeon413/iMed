using iMedDataSource.enums;
using iMedShared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMedDataSource
{
    /// <summary>
    /// 
    /// </summary>
    public class VisitRowModel:
            NotifyModelBase
    {
        private DateTime _DateVisit;
        private eVisitType _VisitType;
        private String _Diagnos;

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
        public DateTime DateVisit
        {
            get 
            {
                return this._DateVisit;
            }

            set 
            {
                if( this._DateVisit != value )
                {
                    this._DateVisit = value;
                    OnPropertyChanged( "Datevisit" );
                }
            }
        }

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
        public String Diagnos
        {
            get {
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientId"></param>
        /// <param name="dateVisit"></param>
        /// <param name="visitType"></param>
        /// <param name="diagnos"></param>
        public VisitRowModel( Int32 id, Int32 clientId, DateTime dateVisit,
                              eVisitType visitType, String diagnos )
        {
            this.Id = id;
            this.ClientId = clientId;
            this._DateVisit = dateVisit;
            this.VisitType = visitType;
            this._Diagnos = diagnos;
        }

        /// <summary>
        /// Ctor default
        /// </summary>
        public VisitRowModel()
        {

        }
    }
}
