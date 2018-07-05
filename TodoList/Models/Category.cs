using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Le champ Name est obligatoire")] //champ obligatoire
        [MinLength(5, ErrorMessage = "Nombre de caracteres insuffisants")]
        //[RegularExpression("^[a-z]$")]
        public string Name { get; set; }

      //  public ICollection<Todo> Todos { get; set; }
    }
}