using siteControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace siteControl.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
       
        [HttpGet]
        public ActionResult MainPage()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult MainPage(Truck truck)
        {
            if (ModelState.IsValid)
            {
                string timeToday = DateTime.Now.ToString("h:mm:ss tt");
                string dateToday = DateTime.Now.ToString("M/dd/yyyy");
                using (var db = new MainDbContext())
                {
                    Claim sessionEmail = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email);
                    string userEmail = sessionEmail.Value;
                    var userIdQuery = db.Users.Where(u => u.Email == userEmail).Select(u => u.ID);
                    var userId = userIdQuery.ToList();

                    var currentUserName = User.Identity.Name;
                    var managername = db.Users.Where(u => u.Name == currentUserName).FirstOrDefault();

                    var dbList = db.Trucks.Create();

                    dbList.DispatchExt = truck.DispatchExt;
                    dbList.TruckID = truck.TruckID;
                    dbList.CurrentLocation = truck.CurrentLocation;
                    dbList.CurrentStatus = truck.CurrentStatus;
                    dbList.Date_Posted = dateToday;
                    dbList.Time_Posted = timeToday;
                    dbList.UserID = managername;

                    db.Trucks.Add(dbList);
                    db.SaveChanges();
                    return RedirectToAction("MyTruck", "User");
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect format has been placed");
                return View(truck);
            }
          

        }

        public ActionResult ViewPage()
        {

            var listTable = new MainDbContext();
            return View(listTable.Trucks.ToList());

        }

        public static string date_posted = "";
        public static string time_posted = "";
        [HttpGet]

        public ActionResult EditPage(int id)
        {
             
         
            var db = new MainDbContext();
            var model1 = new Truck();
            model1 = db.Trucks.Find(id);
            date_posted = model1.Date_Posted;
            time_posted = model1.Time_Posted;

            return View(model1);
        }

        [HttpPost]
        public ActionResult EditPage(Truck list)
        {

            var db = new MainDbContext();
            string timeToday = DateTime.Now.ToString("h:mm:ss tt");
            string dateToday = DateTime.Now.ToString("M/dd/yyyy");
            

            if (ModelState.IsValid)
            {
                list.Time_Edited = timeToday;
                list.Date_Edited = dateToday;
                list.DispatchExt = list.DispatchExt;
                list.TruckID = list.TruckID;
                list.CurrentLocation = list.CurrentLocation;
                list.CurrentStatus = list.CurrentStatus;
                list.Time_Posted = time_posted;
                list.Date_Posted = date_posted;
           
               

                

                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Viewpage", "User");
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new MainDbContext();
            var model = db.Trucks.Find(id);

      
            db.Trucks.Remove(model);
            db.SaveChanges();
            return RedirectToAction("ViewPage", "User");
        }

        public ActionResult MyTruck()
        {
            var db = new MainDbContext();
            var avail = db.Trucks.Where(x => x.UserID.Name == User.Identity.Name).ToList();
            return View(avail);
        }
    }
}