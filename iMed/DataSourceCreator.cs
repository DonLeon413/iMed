using iMedDataSource;
using IMedDataSourceMSSQL;

namespace iMed
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataSourceCreator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IDataSource CreateDataSource()
        {
            return new DataSourceMSSQL();
        }
    }
}
