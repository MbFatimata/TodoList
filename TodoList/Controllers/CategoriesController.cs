﻿using System;
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
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {        
        //ouverture de la connexion a la db
        private TodoListDbContext db = new TodoListDbContext();

       //creation d'une categorie
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

        //Afficher les categories
        [ResponseType(typeof(Category))]
        public List<Category> GetCategory()
        {
            return db.Categories.Where(x => !x.Deleted).ToList(); //retourne la liste des categories non supprimée
        }
        //public IQueryable<Category> GetCategory()
        //{
        //    return db.Categories.Where(x => !x.Deleted); //retourne la liste des categories non supprimée
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        //rechercher une categorie part son ID
        [ResponseType(typeof(Category))]
        [Route("{id:int}")]
        public IHttpActionResult GetCategory(int id)
        {
            var category = db.Categories.SingleOrDefault(x => x.ID == id);
            // autre solution
            // var category = db.Categories.Find(id)
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        //rechercher une categorie part son nom ou une partie de son nom
        [ResponseType(typeof(Category))]
        [Route("{name}")]
        public IQueryable<Category> GetCategory(string name)
        {
            return db.Categories.Where(x => !x.Deleted && x.Name.Contains(name));
        }

        //modifier une categorie
        [ResponseType(typeof(Category))]
        [Route("{id:int}")]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // recherche la category deja existante pour la modifier
            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
            //autre methode
            //var elementAMod = db.Categories.SingleOrDefault(x => x.ID == id);
            //elementAMod.Name = category.Name;
            //db.SaveChanges();
            //return StatusCode(HttpStatusCode.NoContent);
        }

        //effacer une categorie
        [ResponseType(typeof(Category))]
        [Route("api/categories/{id:int}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            var category = db.Categories.SingleOrDefault(x => x.ID == id); // var category = db.Categories.Find(id);
            if (category == null)
                return NotFound();
            //db.Categories.Remove(category);
            category.Deleted = true;
            category.DeletedAt = DateTime.Now;
            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            return Ok(category);
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
