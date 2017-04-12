using HotelBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace HotelBook.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult ViewCustomer()
        {
            HotelDBContext hotelDb = new HotelDBContext();
            List<Customer> range = hotelDb.Viewcustomer();
           return View(range);
        }

        public void setAccept(string email)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            Debug.WriteLine(email);

            string h = email;
           // hotelDb.set(h);
            hotelDb.sendMail(h);
        }
        public void setReject(string email)
        {
            HotelDBContext hotelDb = new HotelDBContext();
           

            string h = email;
            hotelDb.setr(h);
        }
        [HttpPost]
        public ActionResult Search(string searchstring,string searchstring1)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            String name = "Null";
            String city = "Null";
            if (!String.IsNullOrEmpty(searchstring))
            {
                name = searchstring;
            }
            if (!String.IsNullOrEmpty(searchstring1))
            {
                city = searchstring1;
            }
            List<Customer> range = hotelDb.searchCustomer(name,city);
            
            return View("~/Views/Home/SearchView.cshtml",range);
        }
        [HttpGet]
        public ActionResult Search()
        {
            
            return View();
        }
        public ActionResult Package()
        {
            HotelDBContext hotelDb = new HotelDBContext();
            List<Package> range = hotelDb.Viewpackage();
            return View(range);
        }
        [HttpGet]
        public ActionResult AddPackage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPackage(Package pack)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.InsertPack(pack);
            return View("~/Views/Home/Index.cshtml");
        }
        [HttpGet]
        public ActionResult EditPackage(int id)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            Package pack = hotelDb.PackView(id);
            Debug.WriteLine("aaaaa" + pack.id);
            pack.id = id;
            return View(pack);
        }

        [HttpPost]
        public ActionResult EditPack(Package pac)
        {
            Debug.WriteLine(pac.id);
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.EditPack(pac);

            return View("~/Views/Home/Index.cshtml");
        }
        public ActionResult DeletePackage(int id)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.DeletePackage(id);
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Boarding(string searchstring, string searchstring1)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            String state = "Null";
            String city = "Null";
            if (!String.IsNullOrEmpty(searchstring))
            {
                state = searchstring;
            }
            if (!String.IsNullOrEmpty(searchstring1))
            {
                city = searchstring1;
            }
            List<Boarding> range = hotelDb.viewBoarding(state, city);

            return View("~/Views/Home/BoardingView.cshtml", range);
        }  
        [HttpGet]
        public ActionResult Boarding()
        {

            return View();
        }
    }
}