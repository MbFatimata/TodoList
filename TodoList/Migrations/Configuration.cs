using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TodoList.Data;

namespace TodoList.Migrations
{
    public class Configuration : DbMigrationsConfiguration<TodoListDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false; // false pour ne pas que la migration soit appliqué directement à la BASE, false pour que l'on puisse d'abord valider notre commit avant de le pusher dans la BASE
        }
    }
}