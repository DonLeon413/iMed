using iMedDataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMed.intrefaces
{
    /// <summary>
    /// 
    /// </summary>    
    public interface IMultiThreadObservableCollectionConsumer
    {
        /// <summary>
        /// 
        /// </summary>
        void ClearClients( );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        void AddClient( ClientRowModel newItem );

        /// <summary>
        /// 
        /// </summary>
        void ClearVisits( );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        void AddVisit( VisitRowModel newItem );
    }
}
