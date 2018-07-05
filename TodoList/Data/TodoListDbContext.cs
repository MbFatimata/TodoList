using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoList.Models;

namespace TodoList.Data
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext() : base("TodoList") //appelle le constructeur DbContext avec comme parametre TodoList (nom de notre connexionString)
        {
        }

        public DbSet <Category> Categories { get; set; }
    }
}