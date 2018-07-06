using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    [Table("Todos")]
    public class Todo : BaseModel
    {
       // public int ID { get; set; } pas besoin car herite de BaseModel
        //[Column("Name")]
        public string Name { get; set; }
        public bool Done { get; set; }
        public DateTime DeadLine{ get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

    }
}