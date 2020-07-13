namespace UniversityCourseAndResultManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class varsion1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DeptCode = c.String(nullable: false, maxLength: 7),
                        DeptName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Departments");
        }
    }
}
