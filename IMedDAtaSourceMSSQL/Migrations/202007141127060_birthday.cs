namespace IMedDataSourceMSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// 
    /// </summary>
    public partial class birthday : DbMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Clients", "BirthDay", c => c.DateTime(nullable: false));
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Clients", "BirthDay", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
        }
    }
}
