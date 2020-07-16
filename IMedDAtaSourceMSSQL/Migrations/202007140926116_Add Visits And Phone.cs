namespace IMedDataSourceMSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// 
    /// </summary>
    public partial class AddVisitsAndPhone : DbMigration
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitData = c.DateTime(nullable: false),
                        VisitType = c.Int(nullable: false),
                        Diagnos = c.String(maxLength: 25),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            AddColumn("dbo.Clients", "Phone", c => c.String(maxLength: 128));
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Visits", "ClientId", "dbo.Clients");
            DropIndex("dbo.Visits", new[] { "ClientId" });
            DropColumn("dbo.Clients", "Phone");
            DropTable("dbo.Visits");
        }
    }
}
