using ProyectoAccenture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAccenture2.Controllers
{
    public class AutoresController : Controller
    {

        public ActionResult Listar()
        {
            List<Autores> autores = new List<Autores>();
            using (gestionLibrosEntities db = new gestionLibrosEntities())
            {
                autores = db.Autores.ToList();
            }

            return View(autores);
        }


        [HttpPost]
        public ActionResult Listar(ListarViewModel filtros)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();

            IQueryable<Autores> qry = db.Autores;

            if (filtros.FiltroTitulo != null)
            {
                qry = qry.Where(lib => lib.Nombre.Contains(filtros.FiltroTitulo));
            }


            return View(qry.ToList());
        }



        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(string Nombre,string Apellido, string nacionalidad)
        {

            gestionLibrosEntities db = new gestionLibrosEntities();

            Autores autor = new Autores();
            autor.Nombre = Nombre;
            autor.Apellido = Apellido;
            autor.Nacionalidad = nacionalidad;

            db.Autores.Add(autor);

            db.SaveChanges();

            return RedirectToAction("Listar");
        }


        public ActionResult RemoverAutor(int id)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();
            Autores autor = db.Autores.Find(id);
            List<Libros> libros = db.Libros.ToList();

            foreach (var itemLibro in libros)
            {

                itemLibro.Autores.Remove(autor);                   
                
            }

            db.Autores.Remove(autor);


            db.SaveChanges();
            return RedirectToAction("Listar");

        }

        public ActionResult EditarAutor(int id)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();
            Autores autor = db.Autores.Find(id);

            return View(autor);

        }

        [HttpPost]
        public ActionResult EditarAutor(Autores autor)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();

            Autores nuevoAutor = db.Autores.Find(autor.id_autor);
            db.Autores.Remove(nuevoAutor);

            db.Autores.Add(autor);
            db.SaveChanges();


            return RedirectToAction("listar");
        }

    }
}