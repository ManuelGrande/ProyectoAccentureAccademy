using ProyectoAccenture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAccenture2.Controllers
{
    public class GenerosController : Controller
    {

        public ActionResult Listar()
        {
            List<Generos> generos = new List<Generos>();
            using (gestionLibrosEntities db = new gestionLibrosEntities())
            {
                generos = db.Generos.ToList();
            }

            return View(generos);
        }


        [HttpPost]
        public ActionResult Listar(ListarViewModel filtros)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();

            IQueryable<Generos> qry = db.Generos;

            if (filtros.FiltroTitulo != null)
            {
                qry = qry.Where(lib => lib.Genero.Contains(filtros.FiltroTitulo));
            }


            return View(qry.ToList());
        }


        public ActionResult Nuevo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Nuevo(String Genero)
        {

            gestionLibrosEntities db = new gestionLibrosEntities();

            Generos genero = new Generos();
            genero.Genero = Genero;

            db.Generos.Add(genero);

            db.SaveChanges();

            return RedirectToAction("Listar");
        }


        public ActionResult RemoverGenero(int id)  ///NO REMUEVE LIBROS CON RELACION
        {
            gestionLibrosEntities db = new gestionLibrosEntities();
            Generos genero = db.Generos.Find(id);

            List<Libros> listaDb = db.Libros.ToList();
            List<Libros> ListaRemover = new List<Libros>();


            foreach (var item in listaDb)
            {
                if (item.id_genero == id)
                {
                    ListaRemover.Add(item);
                }

            }

            foreach (var item in ListaRemover)
            {
                (db.Libros.Find(item)).id_genero = null;
            }

            db.Generos.Remove(genero);
            db.SaveChanges();
            return RedirectToAction("Listar");

        }

        public ActionResult EditarGenero(int id)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();
            Generos genero = db.Generos.Find(id);

            return View(genero);

        }

        [HttpPost]
        public ActionResult EditarGenero(string genero,int id_genero)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();

            Generos viejoGenero= db.Generos.Find(id_genero);

            db.Generos.Remove(viejoGenero);

            Generos nuevoGenero = new Generos();
            nuevoGenero.Genero = genero;


            db.Generos.Add(nuevoGenero);
            db.SaveChanges();


            return RedirectToAction("listar");
        }



    }
}