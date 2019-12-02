using ProyectoAccenture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAccenture2.Controllers
{
    public class EditorialController : Controller
    {

        public ActionResult Listar()
        {
            List<Editoriales> editoriales = new List<Editoriales>();
            using (gestionLibrosEntities db = new gestionLibrosEntities())
            {
                editoriales = db.Editoriales.ToList();
            }

            return View(editoriales);

        }

        [HttpPost]
        public ActionResult Listar(ListarViewModel filtros)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();

            IQueryable<Editoriales> qry = db.Editoriales;

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
        public ActionResult Nuevo(string Nombre)
        {

            gestionLibrosEntities db = new gestionLibrosEntities();

            Editoriales editorial = new Editoriales();
            editorial.Nombre = Nombre;

            db.Editoriales.Add(editorial);

            db.SaveChanges();

            return RedirectToAction("Listar");
        }

        public ActionResult RemoverEditorial(int id)  
        {
            gestionLibrosEntities db = new gestionLibrosEntities();
            Editoriales editorial = db.Editoriales.Find(id);

            List<Libros> listaDb = db.Libros.ToList();
            List<Libros> ListaRemover = new List<Libros>();


            foreach (var item in listaDb)
            {
                if (item.id_editorial == id)
                {
                    ListaRemover.Add(item);
                }

            }

            foreach (var item in ListaRemover)
            {
                (db.Libros.Find(item)).id_editorial = null; ///ARREGLATE 
            }

            db.Editoriales.Remove(editorial);
            db.SaveChanges();
            return RedirectToAction("Listar");

        }

        public ActionResult EditarEditorial(int id)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();
            Editoriales editorial = db.Editoriales.Find(id);

            return View(editorial);

        }

        [HttpPost]
        public ActionResult EditarEditorial(Editoriales editorial)
        {
            gestionLibrosEntities db = new gestionLibrosEntities();



            Editoriales nuevaEditorial = db.Editoriales.Find(editorial.id_editorial);

            foreach (var item in db.Libros.ToList())
            {
                if (item.id_editorial == nuevaEditorial.id_editorial)
                {
                    item.Editoriales = db.Editoriales.Find(editorial.id_editorial);

                } 
            }

            db.Editoriales.Remove(nuevaEditorial);

            db.Editoriales.Add(editorial);
            db.SaveChanges();


            return RedirectToAction("listar");
        }


    }
}