using Demo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class MarketController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Market
        public ActionResult GetPartial()
        {
            var queryList = db.Goods.Include(x => x.Type).ToList();
            return PartialView("GetPartial", queryList);
        }

        public ActionResult Index()
        {
            ViewBag.TypeList = new SelectList(db.Types, "Id", "Name");         
            return View();
        }


        public ActionResult Add(string Name,int Quntity ,int type_Id)
        {
            if (ModelState.IsValid)
            {
                Goods g = new Goods
                {
                    
                    Name = Name,
                    Quntity = Quntity,
                    TypeId = type_Id
                };
                db.Goods.Add(g);
                db.SaveChanges();
                return Json(g, JsonRequestBehavior.AllowGet);
            }
            return View();
           
        }



        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var query = db.Goods.SingleOrDefault(x => x.Id == Id);
            db.Goods.Remove(query);
            db.SaveChanges();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
       



        [HttpPost]
        public ActionResult Edit(int Id, string Name, int Quntity, int type_Id)
        {
            Goods ts = new Goods
            {
                Name = Name,
                Quntity = Quntity,
                Id = Id,
                TypeId = type_Id
            };
            db.Entry(ts).State = EntityState.Modified;
            db.SaveChanges();
            return Json(ts, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var queryEdit = db.Goods
.SingleOrDefault(x => x.Id == Id);
            return Json(queryEdit, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SearchBY(string text)
        {
            var query = db.Goods.Include(x=>x.Type).Where(x => x.Name.Contains(text)).ToList();
            return PartialView("SearchBY", query);
        }


        [HttpGet]
        public ActionResult Search(string Name,int? Quntity)
        {
            var query = db.Goods.ToList();

            var list = new List<string>();
            var Names = db.Goods.Select(x => x.Name);
            list.AddRange(Names.Distinct());

            

            ViewBag.Name = new SelectList(list);
            ViewBag.Quntity = new SelectList(db.Goods, "Id", "Quntity");

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(x => x.Name == Name).ToList();
            }

            if (Quntity.HasValue)
            {
                query = query.Where(x => x.Quntity == Quntity).ToList();
            }

            return View(query);
        }


        [HttpGet]
        public ActionResult CheckBox()
        {
            var query = db.Names.ToList();
            return View(query);
        }


        [HttpPost]
        public ActionResult CheckBox(int[] Hobby)
        {
            Hobby h = new Hobby();
            for (int i = 0; i < Hobby.Length; i++)
            {
                h.UserId = User.Identity.GetUserId();
                h.HobbyiesId = Hobby[i];
                h.IsChecked = true;
                db.Hobbys.Add(h);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        public ActionResult CascadeList()
        {
            ViewBag.Countrylist = new SelectList(db.Country, "Id", "CountryName"); 
            return View();
        }

        public ActionResult GetStates(int Id)
        {
            var stateslist = db.State.Where(x => x.CountryId == Id).ToList();
            return PartialView("GetStates",stateslist);
        }


        public ActionResult MultiDropDown()
        {
            CategoryViewModel vm = new CategoryViewModel();
           
            var list = db.categories.Select(x => x.CatName).Distinct().ToList();
            vm.GetCatList = list;

              
            return View(vm);

        }
        [HttpPost]
        public ActionResult SaveMulti(CategoryViewModel cat)
        {
            Category c = new Category();
            foreach (var x in cat.GetList)
            {
                c.CatName = x;
                db.categories.Add(c);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}