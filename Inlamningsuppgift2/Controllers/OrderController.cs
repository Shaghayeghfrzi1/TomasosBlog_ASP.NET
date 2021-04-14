using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inlamningsuppgift2.Models;

namespace Inlamningsuppgift2.Controllers
{
    public class OrderController : Controller
    {
        TomasosContext _context;
        public OrderController(TomasosContext content)
        {
            _context = content;
        }
   

        //för att köpa, ta emot kund id, id mat och pris
        public IActionResult Order(int Id, int matrattId, int price)
        {
            var bestallningOld = _context.Set<Bestallning>().SingleOrDefault(x => x.KundId == Id && x.Levererad == false);
            var matratt = _context.Set<Matratt>().Find(matrattId);
            if (bestallningOld != null)
            {
                var bestallningMatrattOld = _context.Set<BestallningMatratt>().SingleOrDefault(x => x.MatrattId == matrattId && x.BestallningId == bestallningOld.BestallningId);
                if (bestallningMatrattOld != null)
                {
                    bestallningMatrattOld.Antal = bestallningMatrattOld.Antal + 1;
                    _context.BestallningMatratts.Update(bestallningMatrattOld);
                    _context.SaveChanges();

                    bestallningOld.Totalbelopp = bestallningOld.Totalbelopp + price;
                    _context.Bestallnings.Update(bestallningOld);
                    _context.SaveChanges();
                }
                else
                {
                    var bestallningMatratt = new BestallningMatratt()
                    {
                        MatrattId = matrattId,
                        BestallningId = bestallningOld.BestallningId,
                        Antal = 1,
                    };
                    _context.BestallningMatratts.Add(bestallningMatratt);
                    _context.SaveChanges();

                    bestallningOld.Totalbelopp = bestallningOld.Totalbelopp + price;
                    _context.Bestallnings.Update(bestallningOld);
                    _context.SaveChanges();

                }



            }
            else
            {
                var bestallningNew = new Bestallning()
                {
                    KundId = Id,
                    BestallningDatum = DateTime.Now,
                    Totalbelopp = 0,
                    Levererad = false,
                };
                _context.Bestallnings.Add(bestallningNew);
                _context.SaveChanges();


                var bestallningMatratt = new BestallningMatratt()
                {
                    MatrattId = matrattId,
                    BestallningId = bestallningNew.BestallningId,
                    Antal = 1,
                };
                _context.BestallningMatratts.Add(bestallningMatratt);
                _context.SaveChanges();


                bestallningNew.Totalbelopp = bestallningNew.Totalbelopp + price;
                _context.Bestallnings.Update(bestallningNew);
                _context.SaveChanges();

            }

            return RedirectToAction("OrderDetalis", "Order", new { Id });
        }

        //listan på varukorgen
        public IActionResult OrderDetalis(int? Id)
        {
            ViewBag.Id = Id;

            var bestallning = _context.Set<Bestallning>().SingleOrDefault(x => x.KundId == Id && x.Levererad == false);
            if (bestallning != null)
            {
                var bestallningMatratt = _context.Set<BestallningMatratt>().Where(x => x.BestallningId == bestallning.BestallningId).ToList();
                ViewBag.bestallningMatratt = bestallningMatratt;

                ViewBag.bestallningTotalbelopp = bestallning.Totalbelopp;
                ViewBag.bestallningId = bestallning.BestallningId;
            }
            else
            {
                return RedirectToAction("Index", "Home", new { Id });
            }


            return View();
        }

        //visar total sum och tummar listan
        public IActionResult Payment(int? Id,int BestallningId)
        {

            ViewBag.Id = Id;

            var bestallning = _context.Bestallnings.Find(BestallningId);
            bestallning.Levererad = true;
            bestallning.BestallningDatum = DateTime.Now;
            _context.Bestallnings.Update(bestallning);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home", new { Id });
            
        }

        //För att ta bort en varor på listan
        public IActionResult Delete(int? Id, int matrattId ,int BestallningId ,int price)
        {

            ViewBag.Id = Id;

            var bestallningMatratt = _context.Set<BestallningMatratt>().SingleOrDefault(x => x.MatrattId == matrattId && x.BestallningId == BestallningId);
            
            _context.BestallningMatratts.Remove(bestallningMatratt);
            _context.SaveChanges();

            var bestallning = _context.Bestallnings.Find(BestallningId);
            bestallning.Totalbelopp = bestallning.Totalbelopp - price;
            _context.Bestallnings.Update(bestallning);
            _context.SaveChanges();

            return RedirectToAction("OrderDetalis", "Order", new { Id });

        }


    }
}
