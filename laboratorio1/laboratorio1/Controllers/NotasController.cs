using laboratorio1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace laboratorio1.Controllers
{
    public class NotasController : Controller
    {
        // GET: Notas
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Calcularnotas(string nombre, double labo1, double par1, double labo2, double par2, double labo3, double par3)
        {
            using(AlumnoNotasEntities db = new AlumnoNotasEntities())
            {
                TblNotasAlumno Agregar = new TblNotasAlumno();

                Agregar.Nombre = nombre;
                Agregar.Lab1 = labo1;
                Agregar.Lab2 = labo2;
                Agregar.Lab3 = labo3;
                Agregar.Parcial1 = par1;
                Agregar.Parcial2 = par2;
                Agregar.Parcial3 = par3;

                double Nperiodo1 = (labo1 * 0.4) + (par1 * 0.6);
                double Nperiodo2 = (labo2 * 0.4) + (par2 * 0.6);
                double Nperiodo3 = (labo3 * 0.4) + (par3 * 0.6);

                Agregar.Periodo1 = Nperiodo1;
                Agregar.Periodo2 = Nperiodo2;
                Agregar.Periodo3 = Nperiodo3;
                Agregar.Final = (Nperiodo1 + Nperiodo2 + Nperiodo3) / 3;

                db.TblNotasAlumno.Add(Agregar);
                db.SaveChanges();

               
            }

            return Redirect("/Notas/Resultado");

        }
        public ActionResult Resultado()
        {
            using(AlumnoNotasEntities db = new AlumnoNotasEntities())
            {
                TblNotasAlumno notasEstudiantes = new TblNotasAlumno();

                var resultado = db.Set<TblNotasAlumno>().OrderByDescending(t => t.Id).FirstOrDefault();
                
                return View(resultado);
            }

            
        }
        public ActionResult NotasEstudiante()
        {
            using (AlumnoNotasEntities db = new AlumnoNotasEntities())
            {
                var notasEstudiantes = db.TblNotasAlumno.ToList();
                return View(notasEstudiantes);
            }
                
                
        }
    }
   
}