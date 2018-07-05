using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class CategoriesController : ApiController
    {
        private TodoListDbContext db = new TodoListDbContext();

        //ouverture de la connexion a la db
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.ID }, category);
        }

        [ResponseType(typeof(Category))]
        public List<Category> GetCategory()
        {
            return (from x in db.Categories select x).ToList();
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var test = db.Categories.SingleOrDefault(x => x.ID == id);
            return Ok(test);
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }
            var elementAMod = db.Categories.SingleOrDefault(x => x.ID == id);
            elementAMod.Name = category.Name;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            var test = db.Categories.SingleOrDefault(x => x.ID == id);
            if (test == null)
                return BadRequest();
            db.Categories.Remove(test);
            db.SaveChanges();


            return Ok(test);
        }

        //réecriture de la methode dispose pour libérer en mémoire le DbContext et donc la connexion
        // override utilisé pour réecriture
        // methode dispose appelée lorsque que IIS n'utilise plus le controller
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose(); // libère le db context
            base.Dispose(disposing);
        }
    }
}
