using IMedDataSourceMSSQL.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMedDataSourceMSSQL
{
    /// <summary>
    /// 
    /// </summary>
    public class iMedDataContext: DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public iMedDataContext( ):
            base( "DefaultConnection" )
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            Database.SetInitializer( new MigrateDatabaseToLatestVersion<iMedDataContext,
                                                IMedDataSourceMSSQL.Migrations.Configuration>( true ) );
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Client> Clients
        {
            get; 
            set; 
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Visit> Visits
        {
            get;
            set;
        }
    }
}
