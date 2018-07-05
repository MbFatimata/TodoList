namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTodo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Todos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Done = c.Boolean(nullable: false),
                        DeadLine = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        Description = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Todos", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Todos", new[] { "CategoryID" });
            DropTable("dbo.Todos");
        }
    }
}
