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
    public class ClientRowModel:
            NotifyModelBase
    {
        private Int32 _id;
        private String _Name;
        private String _SecondName;
        private String _ThirdName;

        /// <summary>
        /// 
        /// </summary>
        public Int32 Id
        {
            get
            {
                return this._id;
            }
            set
            {
                if( this._id != value )
                {
                    this._id = value;
                    OnPropertyChanged( "Id" );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                if( 0 != String.Compare( this._Name, value, true ) )
                {
                    this._Name = value;
                    OnPropertyChanged( "Name" );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String SecondName
        {
            get
            {
                return this._SecondName;
            }

            set
            {
                if( 0 != String.Compare( this._SecondName, value, true ) )
                {
                    this._SecondName = value;
                    OnPropertyChanged( "SecondName" );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ThirdName
        {
            get
            {
                return this._SecondName;
            }

            set
            {
                if( 0 != String.Compare( this._ThirdName, value, true ) )
                {
                    this._ThirdName = value;
                    OnPropertyChanged( "ThirdName" );
                }
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <param name="thirdName"></param>
        public ClientRowModel( Int32 id, String name, String secondName, String thirdName )
        {

            this.Id = id;

            this.Name = name;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
        }
    }
}
