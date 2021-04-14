using Inlamningsuppgift2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inlamningsuppgift2.Controllers
{
    public class UserManagerController : Controller
    {
        TomasosContext _context;
        public UserManagerController(TomasosContext content)
        {
            _context = content;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Kund model)
        {
            if (ModelState.IsValid)
            {
                var user =_context.Kunds.Add(model);
                _context.SaveChanges();
                ViewBag.Error = "Grattis! Du är medlem nu.";
                return RedirectToAction("Index", "Home", new { });
            }
            else
            {
                ViewBag.Error = "Försök igen.";
            }
            return View();

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Kund model)
        {

            var result = _context.Kunds.SingleOrDefault(k => k.AnvandarNamn == model.AnvandarNamn &&
                                                         k.Losenord == model.Losenord);
            if (result != null)
            {
                //return View("Order", result);
                return RedirectToAction("Index", "Home", new { Id = result.KundId });
            }
            else
            {
                ViewBag.Error = "Din användarnamn eller lösenord är fel. Du är inte registrerad på den hät sida.";
                return View(model);
            }

        }

        public IActionResult Update(int Id)
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

            var user = _context.Kunds.SingleOrDefault(x => x.KundId == Id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Update(Kund kundUpdate)
        {
            Kund kundOrg = _context.Kunds.SingleOrDefault(k => k.KundId == kundUpdate.KundId);

            _context.Entry(kundOrg).CurrentValues.SetValues(kundUpdate);

            _context.SaveChanges();
            TempData["Message"] = "Dina uppgifter är uppdateras.";
            return RedirectToAction("Index", "Home", new { Id = kundOrg.KundId });


        }
    }
}
