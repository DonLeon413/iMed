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
    public class IMedDataSourceException:
                Exception
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message"></param>
        public IMedDataSourceException( String message ):
                base( message )
        {

        }
    }
}
