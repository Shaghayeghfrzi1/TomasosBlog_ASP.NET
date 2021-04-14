using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inlamningsuppgift2.Models;

namespace Inlamningsuppgift2.Controllers
{
    public class HomeController : Controller
    {
        TomasosContext _context;
        public HomeController(TomasosContext content)
        {
            _context = content;
        }

        /// <summary>
        /// Ta emot kundens yp av mat och beräknar total av summan, sen visar greger med hjälp av kategorier
        /// </summary>
        /// <param name="Id">kund</param>
        /// <param name="matrattType"> typ av mat</param>
        /// <returns></returns>
        public IActionResult Index(int? Id,int? matrattType)
        {
            ViewBag.Id = Id;
            if (Id != null)
            {
                var bestallningTotalbelopp = _context.Set<Bestallning>().SingleOrDefault(x => x.KundId == Id && x.Levererad == false);
                if (bestallningTotalbelopp != null)
                {
                    ViewBag.bestallningTotalbelopp = bestallningTotalbelopp.Totalbelopp;
                }
                else
                {
                    ViewBag.bestallningTotalbelopp = "0";
                }
            }

            var matrattTyp = _context.Set<MatrattTyp>().ToList();
            ViewBag.matrattTyp = matrattTyp;

            

            if (matrattType != null)
            {
                var matratt = _context.Set<Matratt>().Where(x =>x.MatrattTyp == matrattType).ToList();
                ViewBag.matratt = matratt;

                var Beskrivning = _context.MatrattTyps.Find(matrattType).Beskrivning;
                ViewBag.Beskrivning = Beskrivning;
            }
            else
            {
                var matratt = _context.Set<Matratt>().ToList();
                ViewBag.matratt = matratt;
            }


            return View();
        }

      
    }
}
