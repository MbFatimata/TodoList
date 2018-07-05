using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;

namespace TodoList.Controllers
{
    public class TestController : ApiController
    {
        public class TestModel
        {
            public int ID { get; set; }
            public string Commentaire { get; set; }
        }
        //GET: api/test
        public List<TestModel> GetTests()
        {
            List<TestModel> liste = new List<TestModel>();
            //liste.Add(new TestModel { ID = 42, Commentaire = "la réponse" });
            //liste.Add(new TestModel { ID = 39, Commentaire = "température actuelle" });
            // liste.Add(new TestModel { ID = 98, Commentaire = "au hasard" });

            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            return (from x in doc.Descendants("Test")
                    select new TestModel
                    {
                        ID = int.Parse(x.Element("ID").Value),
                        Commentaire = x.Element("Commentaire").Value
                    }).ToList();

            //autre solution
            //return doc.Descendants("Test").Select(x => new TestModel
            //{
            //    ID = int.Parse(x.Element("ID").Value),
            //    Commentaire = x.Element("Commentaire").Value

            //}).ToList();

            //autre solution 
            //var elements = doc.Root.Elements();
            //foreach (var item in elements)
            //{
            //    var test = new TestModel
            //    {
            //        ID = int.Parse(item.Element("ID").Value),
            //        Commentaire = item.Element("Commentaire").Value
            //    };
            //    liste.Add(test);
            //}
            //return liste;

        }

        //GET: api/test/42
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult GetTest(int id)
        {
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            //Autre solution
            //var requete = from x in doc.Descendants("Test")
            //        select new TestModel
            //        {
            //            ID = int.Parse(x.Element("ID").Value),
            //            Commentaire = x.Element("Commentaire").Value
            //        };
            //var requete2 = (from x in requete
            //               where x.ID == id
            //               select x).SingleOrDefault();
            //if(requete2 == null)
            //    return NotFound();
            //return Ok(requete2);

            
            TestModel test = null;
            //var test = doc.Descendants("Test").SingleOrDefault(x => int.Parse(x.Element("ID").Value)==id);
            var elements = doc.Root.Elements();
            foreach (var item in elements)
            {
                if (int.Parse(item.Element("ID").Value) == id)
                {
                    test = new TestModel
                    {
                        ID = int.Parse(item.Element("ID").Value),
                        Commentaire = item.Element("Commentaire").Value
                    };
                }
            }
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }



        //POST: api/test
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult PostTest(TestModel test) // sert à enregistrer les donnees donc besoin des infos du test à enregistrer
        {

            //XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            //var element = doc.Root.Elements();



            //if (test.ID != 0) //retourne une erreur car pas de commentaire au format JSON, ajout d'un body dans postman pour l'execution du test
            //{
            //    return BadRequest(); //car id existe déjà donc pas d'ajout possible sur cet id
            //}
            //test.ID = 101; //base retourne un autre id disponible

            //return CreatedAtRoute("DefaultApi", new { id = test.ID }, test); // nom de la route (dans WebApiConfig), creation du nouvel id associé au test

            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            var idMax = doc.Descendants("Test").Max(x => int.Parse(x.Element("ID").Value));
            idMax++;
            test.ID = idMax;
            XElement element = new XElement("Test");
            //Creation des balises enfants "ID" et "Commentaire avec les valeurs
            element.Add(new XElement("ID", test.ID));
            element.Add(new XElement("Commentaire", test.Commentaire));
            //Ajouter la nouvelle balise dans la balise "Tests"
            doc.Element("Tests").Add(element);
            //Sauvegarder le fichier
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            //Renvoyer un code 201 "Created" avec l'objet mis à jour
            return CreatedAtRoute("DefaultApi", new { id = test.ID }, test);
        }

        //DELETE: api/test
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult DeleteTest(int id)
        {
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            var test = doc.Descendants("Test").SingleOrDefault(x => int.Parse(x.Element("ID").Value)==id);
            if (test == null)
                return BadRequest(); 
            test.Remove();
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            return Ok(test);
        }

        //PUT: api/test
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult PutTest(int id, TestModel test)
        {
            if (id != test.ID)
            {
                return BadRequest();
            }
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            var elementAMod = doc.Descendants("Test").SingleOrDefault(x => int.Parse(x.Element("ID").Value) == id);
            if (elementAMod == null)
                return BadRequest();
            elementAMod.Element("Commentaire").Value = test.Commentaire;
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}

