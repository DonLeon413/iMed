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
    public interface IDataSource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="findContext"></param>
        /// <returns></returns>
        IEnumerable<ClientRowModel> LoadClients( String findContext = null );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ClientInfoModel LoadClientInfo( Int32 id );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        void SaveClientInfo( ClientInfoModel model );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void DeleteClientById( Int32 id );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        IEnumerable<VisitRowModel> LoadVisits( Int32 clientId );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        VisitInfoModel LoadVisitInfo( Int32 id );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void SaveVisitInfo( VisitInfoModel model );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void DeleteVisitById( Int32 id );
    }
}
