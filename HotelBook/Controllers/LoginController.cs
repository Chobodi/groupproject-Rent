using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using HotelBook.Models;

namespace HotelBook.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        public void index()
        {
            String password = Request["Password"].ToString();
            String email = Request["Email"].ToString();
            Console.Write(password+email);
            
        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Customer customer)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.Insert(customer);
            return View();
        }
        public ActionResult FirstSignup()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Signupboarding()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Signupboarding(Boarding boarding)
        {
            HotelDBContext hotelDb = new HotelDBContext();
            hotelDb.Insertboarding(boarding);
            return View();
        }
    }
}