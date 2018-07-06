using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    public abstract class BaseModel //abstract car ne peut pas etre instanciee
    {
        //[Key]
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        public bool Deleted { get; set; } 

        public DateTime? DeletedAt { get; set; }//int datetime bool type primitif donc not null, mettre nullable ou ? pour appliquer un NULL

    }
}