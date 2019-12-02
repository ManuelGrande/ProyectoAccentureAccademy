
using ProyectoAccenture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAccenture.Controllers
{
    public class LibrosController : Controller
    {
        public ActionResult Listar()
        {
            List<Libros> libros = new List<Libros>();

            gestionLibrosEntities db = new gestionLibrosEntities();

            libros = db.Libros.ToList();


            return View(libros);
        }

        [HttpPost]
        public ActionResult Listar(ListarViewModel filtros)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();

            IQueryable<Libros> qry = db.Libros;

            if (filtros.FiltroTitulo != null)
            {
                qry = qry.Where(lib => lib.Titulo.Contains(filtros.FiltroTitulo));
            }

            if (filtros.FiltroAutor.HasValue)
            {
                qry = qry.Where(lib =>
                    lib.Autores.Any(
                           aut => aut.id_autor.Equals(filtros.FiltroAutor.Value)
                           )
                );
            }

            return View(qry.ToList());
        }


        public ActionResult Nuevo()
        {

            ViewBag.TituloPagina = "Crear un libro.";
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(string titulo, int año, string sinopsis, IEnumerable<int> autores, int editoriales, int generos)
        {

            gestionLibrosEntities db = new gestionLibrosEntities();


            Libros libro = new Libros();

            if(autores != null)
            {

            foreach (int autorActual in autores)
            {
                Autores autor = db.Autores.Find(autorActual);
                libro.Autores.Add(autor);
            }
            }

            libro.Año = año;
            libro.Titulo = titulo;
            libro.Sinopsis = sinopsis;
            if(generos != -1)
            {
            libro.Generos = db.Generos.Find(generos);

            }
            if(editoriales != -1)
            {
            libro.Editoriales = db.Editoriales.Find(editoriales);

            }


            db.Libros.Add(libro);
            db.SaveChanges();



            return RedirectToAction("Listar");

        }

        public ActionResult RemoverLibros(int id)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();
            Libros libro = db.Libros.Find(id);




            db.Libros.Remove(libro);
            db.SaveChanges();
            return RedirectToAction("Listar");


        }
        public ActionResult EditarLibro(int id)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();
            
            Libros libro = db.Libros.Find(id);



            return View("EditarLibros", libro);
        }


        [HttpPost]
        public ActionResult EditarLibro(IEnumerable<int> autores, Libros libro)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();

            Libros nuevoLibro = db.Libros.Find(libro.id_libro);
            db.Libros.Remove(nuevoLibro);
            

            libro.Autores.Clear();
            foreach (int autorActual in autores)
            {                   
                Autores autor = db.Autores.Find(autorActual);
                libro.Autores.Add(autor);
               
            }

           
            db.Libros.Add(libro);


            db.SaveChanges();
            return RedirectToAction("listar");

        }


       




    }
}


    






