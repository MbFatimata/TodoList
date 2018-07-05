namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTodo1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Todos", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Todos", "Description", c => c.Int(nullable: false));
        }
    }
}
