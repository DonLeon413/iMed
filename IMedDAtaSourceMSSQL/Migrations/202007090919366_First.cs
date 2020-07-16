namespace IMedDataSourceMSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// 
    /// </summary>
    public partial class First : DbMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        SecondName = c.String(nullable: false, maxLength: 20),
                        ThirdName = c.String(nullable: false, maxLength: 25),
                        BirthDay = c.DateTime(nullable: false, storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            DropTable("dbo.Clients");
        }
    }
}
