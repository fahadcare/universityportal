namespace UniversityCourseAndResultManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class varsion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Date", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Date", c => c.DateTime(nullable: false));
        }
    }
}
