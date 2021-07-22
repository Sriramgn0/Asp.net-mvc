using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Website.Models;
using University_Website.Context;
using University_Website.Controllers;
using System.Data.Entity;

namespace University_Website.Controllers
{
    public class UserRegistrationController : Controller
    {
        // GET: UserRegistration
        UniversityContext context = new UniversityContext();

        public ActionResult Index()
        {
            return View();
           // return RedirectToAction("Login");
        }
           
        public ActionResult Login1()
        {
            if (Session["Adminid"] != null)
            {
                return RedirectToAction("DashBoard", "AdminDashboard");
            }
            else if (Session["userid"] != null)
            {
                return RedirectToAction("DashBoard", "UserDashboard");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login1(UserRegistration user)
        {
            var AdminLogin = context.UserRegistrations.SingleOrDefault(x => x.UserID == user.UserID && user.Password == x.Password && x.UserType=="Admin" );
            if (AdminLogin != null)
            {
                Session["Adminid"] = user.UserID;
                return RedirectToAction("DashBoard", "AdminDashboard");
            }

            else
            {
                var UserLogin = context.UserRegistrations.SingleOrDefault(x => x.UserID == user.UserID && user.Password == x.Password && x.UserType == "Regular");
                if (UserLogin != null)
                {
                    ViewBag.Message = "Please enter correct login id and password";
                    ViewBag.triedonce = "yes";

                    Session["userid"] = user.UserID;
                    return RedirectToAction("DashBoard", "UserDashboard");
                }
                else
                {
                    ViewBag.triedonce = "yes";
                    return View();
                }
            }

        }

      

        // GET: UserRegistration/Create
        public ActionResult Create1()
        {

            return View();
        }

        // POST: UserRegistration/Create
  [HttpPost]
        public ActionResult Create1(UserRegistration user)
        {
            var Excistance_user = context.UserRegistrations.SingleOrDefault(x => x.Contact_Number == user.Contact_Number || user.Email == x.Email);
           
          if (Excistance_user != null)
            {
                ViewBag.Message = "User already exist";
                return View();
            }
            else
            {
                try
                {
                    // TODO: Add insert logic here
                    user.UserType = "Regular";
                    user.Security_Question1 = UserRegistration.Questions.FavFood;
                    user.Security_Question2 = UserRegistration.Questions.FavFood;
                    user.Security_Question3 = UserRegistration.Questions.FavFood;
                    context.UserRegistrations.Add(user);
                    context.SaveChanges();
                    Session["newid"] = user.UserID;
                    return RedirectToAction("Partial");
                }

                catch
                {
                    ViewBag.Message = "Unable to create your Account \n please try again later";
                    return View();
                }
            }
        }



        public ActionResult Partial()
        {
            ViewBag.Message = "start";
            return View();
        }

        [HttpPost]
        public ActionResult Partial(UserRegistration obj)
        {
            var userid = Convert.ToInt32(Session["newid"]);
            if (Session["newid"] == null)
            {
                return RedirectToAction("Index");
            }
            else
                {
            
                        
                        var userdata = context.UserRegistrations.Find(userid);
                        userdata.Security_Question1 = obj.Security_Question1;
                        userdata.Security_Answer1 = obj.Security_Answer1;
                        userdata.Security_Question2 = obj.Security_Question2;
                        userdata.Security_Answer2 = obj.Security_Answer2;
                        userdata.Security_Question3 = obj.Security_Question3;
                        userdata.Security_Answer3 = obj.Security_Answer3;

                        context.Entry(userdata).State = EntityState.Modified;
                        context.SaveChanges();           
                        ViewBag.User_Id = userdata.UserID;
                        ViewBag.User_Name = userdata.First_Name;
                        ViewBag.Message = "Your account created successfully";
                        Session.Clear();
                        System.Web.HttpContext.Current.Session.RemoveAll();
                return View();
                }

                       
        }


        public ActionResult logout(int id)
        {
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();
            return RedirectToAction("Index");
        }

        public ActionResult ForgottenPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgottenPassword(UserRegistration user)
        {
            var Userdata = context.UserRegistrations.FirstOrDefault(x => x.Email == user.First_Name || x.Contact_Number == user.First_Name);
            if (Userdata != null)
            {
                return RedirectToAction("SecurityQuestions", "UserRegistration", new { userid = Userdata.UserID });
            }
            else
            {
                ViewBag.triedonce = "yes";
                return View();
            }        
        }

        public ActionResult SecurityQuestions(int userid)
        {
            var UserQuestions = context.UserRegistrations.Find(userid);
            ViewBag.Userdata = UserQuestions;
            Session["FP"] = userid;
            return View();
        }

        [HttpPost]
        public ActionResult SecurityQuestions(UserRegistration user)
        {
            var userdata = context.UserRegistrations.Find(Convert.ToInt32(Session["FP"]));
            if (userdata == null)
            {
                return RedirectToAction("ForgottenPassword");
            }
            else
            {
                if(user.Security_Answer1 == userdata.Security_Answer1 && user.Security_Answer2 == userdata.Security_Answer2 && user.Security_Answer3 == userdata.Security_Answer3)
                {
                    return RedirectToAction("ResetPassword", "UserRegistration", new { userid = userdata.UserID });
                }

                else
                {
                    ViewBag.wrongans = "yes";
                    ViewBag.Userdata = userdata;
                    return View();
                }
                    
            }
        }

        public ActionResult ResetPassword(int userid)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(UserRegistration user)
        {
            var userdata = context.UserRegistrations.Find(Convert.ToInt32(Session["FP"]));
            if (userdata == null)
            {
                return RedirectToAction("ForgottenPassword");
            }
            else
            {
                userdata.Password = user.Password;
                userdata.ConfirmPassword = user.ConfirmPassword;
               
                context.Entry(userdata).State = EntityState.Modified;
                context.SaveChanges();
                ViewBag.User_Id = userdata.UserID;
                ViewBag.User_Name = userdata.First_Name;
                return View();
            }
        }

        public ActionResult CreateAdmin()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateAdmin(UserRegistration user)
        {
            var Excistance_user = context.UserRegistrations.SingleOrDefault(x => x.Contact_Number == user.Contact_Number || user.Email == x.Email);

            if (Excistance_user != null)
            {
                ViewBag.Message = "User already exist";
                return View();
            }
            else
            {
                try
                {
                    // TODO: Add insert logic here
                    user.UserType = "Admin";
                    user.Security_Question1 = UserRegistration.Questions.FavFood;
                    user.Security_Question2 = UserRegistration.Questions.FavFood;
                    user.Security_Question3 = UserRegistration.Questions.FavFood;
                    context.UserRegistrations.Add(user);
                    context.SaveChanges();
                    Session["newid"] = user.UserID;
                    return RedirectToAction("Partial");
                }

                catch
                {
                    ViewBag.Message = "Unable to create your Account \n please try again later";
                    return View();
                }
            }
        }


    }
}
