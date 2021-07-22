using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Website.Context;
using University_Website.Models;
using University_Website.Controllers;
using System.Data.Entity;

namespace University_Website.Controllers
{
    public class AdminDashboardController : Controller
    {
        // GET: AdminDashboard
        UniversityContext context = new UniversityContext();
        public ActionResult DashBoard()
        {
            if (Session["Adminid"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserRegistration");
            }
        }
        public ActionResult ViewGrievances()
        {

            return View(context.Grievancess.Where(a => a.Status == Grievances.grief.New || a.Status == Grievances.grief.Progress).ToList());
        }
       

        public ActionResult UpdateGrievanace(int id)
        {
            var SList1 = new List<string>() { "Progress", "Resolved" };
            ViewBag.StatusList1 = SList1;
            var tempGrv = context.Grievancess.Find(id);
            ViewBag.Grv = tempGrv;
            return View();
        }
        [HttpPost]
        public ActionResult UpdateGrievanace(int id, Grievances obj)
        {

            var tempGrv = context.Grievancess.Find(id);
            tempGrv.Status = obj.Status;
            tempGrv.DeadLine = obj.DeadLine;
            tempGrv.AdminReview = obj.AdminReview;
            context.Entry(tempGrv).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Dashboard");

        }
        public ActionResult ViewIssues()
        {

            return View(context.Issues.ToList());
        }
        public ActionResult UpdateIssues(int id)
        {
            var tempI = context.Issues.Find(id);
            ViewBag.Iss = tempI;
            return View();
        }
        [HttpPost]
        public ActionResult UpdateIssues(int id, Issue obj)
        {

            var tempIss = context.Issues.Find(id);
            var AdminDetails = context.UserRegistrations.SingleOrDefault(x => x.UserType == "Admin");


            tempIss.AdminReview = obj.AdminReview;
            tempIss.Contact = AdminDetails.Contact_Number;
            context.Entry(tempIss).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Dashboard");

        }




        public ActionResult CreateEvent()
        {
            if (Session["Adminid"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserRegistration");
            }


        }
        [HttpPost]
        public ActionResult CreateEvent(Events obj)
        {
            obj.status = "Not started";
            if (obj.EndDate < obj.StartDate)
            {
                ViewBag.Message1 = "End date cannot be before Start Date!";
                return View();
            }
            else if (obj.StartDate < DateTime.Now)
            {
                ViewBag.Message1 = "Enter valid start date";
                return View();
            }
            else
            {
                if (obj.StartDate == DateTime.Now)
                {
                    obj.status = "Started";
                }
                else
                {
                    obj.status = "Not started";
                }


                context.Eventss.Add(obj);

                context.SaveChanges();
                ViewBag.Message = "Event created successfully!";
                return View();
            }
        }

        public ActionResult logout()
        {
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();
            return RedirectToAction("Index", "UserRegistration");
        }

        public ActionResult AddClub()
        {
            if (Session["Adminid"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserRegistration");
            }
        }

        [HttpPost]
        public ActionResult AddClub(Club obj)
        {
            if(obj.ClubName!=null)
            {
                context.Clubs.Add(obj);
                context.SaveChanges();
                return RedirectToAction("ViewClubList");
            }
            else
            {
                ViewBag.Message = "Enter Club Name";
                return View();
            }
        }

        public ActionResult ViewClubList()
        {
            return View(context.Clubs.ToList());
        }

        public ActionResult AddCategory()
        {
            if (Session["Adminid"] == null)
            {
                ViewBag.ClubList = context.Clubs.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserRegistration");
            }
        }

        [HttpPost]
        public ActionResult AddCategory(Category obj)
        {
            if(obj.CategoryName!=null)
            {
                context.Categories.Add(obj);
                context.SaveChanges();
                return RedirectToAction("ViewCategoryList");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ViewCategoryList()
        {
            return View(context.Categories.ToList());
        }


        public ActionResult ViewClubMembers()
        {

            return View(context.ClubMembers.ToList());
        }


        public ActionResult UpdateD(int id)
        {
            var tempGrv = context.ClubMembers.Find(id);
            ViewBag.Grv = tempGrv;
            return View();

        }

        [HttpPost]
        public ActionResult UpdateD(int id, ClubMember obj)
        {

            var tempGrv = context.ClubMembers.Find(id);
            tempGrv.Designation = obj.Designation;

            context.Entry(tempGrv).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Dashboard");

        }

        public ActionResult CreateSS()
        {
            if (Session["Adminid"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserRegistration");
            }
        }
        [HttpPost]
        public ActionResult CreateSS(SocialService obj)
        {
            context.SocialServices.Add(obj);
            context.SaveChanges();
            ViewBag.Message = "Social service created successfully!";
            return View();
        }


        public ActionResult Viewperticularclubmember(int id)
        {
            var clubmem = context.ClubMembers.Where(a => a.ClubId == id).ToList();
            var alluserdat = new List<UserRegistration>();
            foreach (var i in clubmem)
            {
                var userdata = context.UserRegistrations.Find(i.UserId);
                alluserdat.Add(userdata);
            }
            ViewBag.clubid = id;
            return View(alluserdat);
        }
         public ActionResult Removemember(int id,int cid)
         {
               var clublistwithmem = context.ClubMembers.Where(a=>a.ClubId==cid).ToList();
               var mem = clublistwithmem.FirstOrDefault(a => a.UserId == id);
               var ToDelete = context.ClubMembers.Remove(mem);
               context.SaveChanges();
               return RedirectToAction("Viewperticularclubmember",new { id = cid });         
        } 

        public ActionResult UpdateDesignation(int id, int cid)
        {
            var mem = context.ClubMembers.Where(a => a.ClubId == cid && a.UserId ==id).ToList();
            ViewBag.mem = mem;
            return View();
        }
        public ActionResult ViewSS()
        {

            return View(context.SocialServices.ToList());
        }

        public ActionResult ViewVolunteer(int id)
        {
            var SSP = context.SocialServiceParticipantss.Where(a => a.SocialServiceId == id).ToList();
            return View(SSP);

        }
        public ActionResult UpdateSS(int id)
        {
            var ssData = context.SocialServices.Find(id);
            ViewBag.Data = ssData;
            return View();

        }
        [HttpPost]
        public ActionResult UpdateSS(SocialService obj)
        {
            var oldData = context.SocialServices.Find(obj.SocialServiceId);
            /* if(obj.NoOfVolunteer == 0)
            {
                obj.NoOfVolunteer = oldData.NoOfVolunteer;
            }
            if(obj.EventDate < DateTime.Now || obj.EventDate < oldData.EventDate)
            {
                obj.EventDate = oldData.EventDate;
            } */

            oldData.NoOfVolunteer = obj.NoOfVolunteer;
            oldData.EventDate = obj.EventDate;
            context.Entry(oldData).State = EntityState.Modified;
            context.SaveChanges();
            ViewBag.Data = oldData;
            ViewBag.Message = "Updated Successfully!";
            return View();
        }


        public ActionResult Socialservicereport()
        {
            return View(context.SocialServices.ToList());
        }

        public ActionResult SocialServicereportspdf()
        {
            return new Rotativa.ActionAsPdf("Socialservicereport");
        }

        public ActionResult Perticularssreportpdf(int id)
        {
            int A = id;
            return new Rotativa.ActionAsPdf("Perticularsocialservicereport", new { id = A });
        }
    

        public ActionResult Perticularsocialservicereport(int id)
        {
            var userdata = context.SocialServiceParticipantss.Where(a => a.SocialServiceId == id).ToList();
            var userlist = new List<UserRegistration>();
            foreach(var i in userdata)
            {
                var d = context.UserRegistrations.Find(i.UserID);
                userlist.Add(d);
            }

            ViewBag.socialservicedata = context.SocialServices.Find(id);
            return View(userlist);
        }


        public ActionResult Reports()
        {
            return View();
        }



        public ActionResult EventReport()
        {
            return View(context.Eventss.ToList());
        }

       public ActionResult PdfEventReport(int id)
        {
            var date = DateTime.Now.AddMonths(-id);
            var eventslist = context.Eventss.Where(a => a.StartDate >= date).ToList();
            if(id==1)
            {
                ViewBag.type = "Events Monthly Report";
            }
            else if (id == 6)
            {
                ViewBag.type = "Events HalfYear Report";
            }
            else if (id == 12)
            {
                ViewBag.type = "Events Yearly Report";
            }
            return View(eventslist);
        }

       public ActionResult EventReportpdf(int id)
        {
            int A = id;
            return new Rotativa.ActionAsPdf("PdfEventReport", new { id = A });
        }




        public ActionResult CampaignReport()
        {
            return View(context.Grievancess.ToList());
        }

        public ActionResult CampaignReportpdf(int id)
        {
            int A = id;
            return new Rotativa.ActionAsPdf("PdfCampaignReport", new { id = A });
        }

        public ActionResult PdfCampaignReport(int id)
        {
            var date = DateTime.Now.AddMonths(-id);
            var campaignlist = context.Grievancess.Where(a => a.DeadLine >= date);
            if (id == 1)
            {
                ViewBag.type = "Campaign Monthly Report";
            }
            else if (id == 6)
            {
                ViewBag.type = "Campaign HalfYear Report";
            }
            else if (id == 12)
            {
                ViewBag.type = "Campaign Yearly Report";
            }
            return View(campaignlist);
        }




       public ActionResult SuggestionsReport()
        {
            return View(context.SocialServices.ToList());
        }


        public ActionResult SuggestionReportpdf(int id)
        {
            int A = id;
            return new Rotativa.ActionAsPdf("PdfSuggestionReport", new { id = A });
        }

        public ActionResult PdfSuggestionReport(int id)
        {

            var date = DateTime.Now.AddMonths(-id);
            var suggestionlist = context.SocialServices.Where(a => a.EventDate >= date);
            if (id == 1)
            {
                ViewBag.type = "suggestion Monthly Report";
            }
            else if (id == 6)
            {
                ViewBag.type = "suggestion HalfYear Report";
            }
            else if (id == 12)
            {
                ViewBag.type = "suggestion Yearly Report";
            }
            return View(suggestionlist);
        }

        public ActionResult AttendencesReport()
        {
            
            ViewBag.SocialserviceParticipants = context.SocialServiceParticipantss.ToList();
            return View(context.EventParticipantss.ToList());
        }

        public ActionResult EventpartReportpdf(int id)
        {
            int A = id;
            return new Rotativa.ActionAsPdf("PdfEventpartReport", new { id = A });
        }

        public ActionResult PdfEventpartReport(int id)
        {
            var date = DateTime.Now.AddMonths(-id);
            var eventlist = context.Eventss.Where(a => a.StartDate > date).ToList();
            var eventpartlist = new List<EventParticipants>(); 
            foreach(var i in eventlist)
            {
                var j = context.EventParticipantss.FirstOrDefault(a => a.EventID == i.EventId);
                eventpartlist.Add(j);
            }
            if (id == 1)
            {
                ViewBag.type = "Event participants Monthly Report";
            }
            else if (id == 6)
            {
                ViewBag.type = "Event participants HalfYear Report";
            }
            else if (id == 12)
            {
                ViewBag.type = "Event participants Yearly Report";
            }
            return View(eventpartlist);
        }

        public ActionResult sspartReportpdf(int id)
        {
            int A = id;
            return new Rotativa.ActionAsPdf("PdfSocialservicepartReport", new { id = A });
        }

        public ActionResult PdfSocialservicepartReport(int id)
        {
            var date = DateTime.Now.AddMonths(-id);
            var sslist = context.SocialServices.Where(a => a.EventDate > date).ToList();
            var sspartlist = new List<SocialServiceParticipants>();
            foreach (var i in sslist)
            {
                var j = context.SocialServiceParticipantss.FirstOrDefault(a => a.SocialServiceId == i.SocialServiceId);
                sspartlist.Add(j);
            }
            if (id == 1)
            {
                ViewBag.type = "Social Service participants Monthly Report";
            }
            else if (id == 6)
            {
                ViewBag.type = "Social Service participants HalfYear Report";
            }
            else if (id == 12)
            {
                ViewBag.type = "Social Service participants Yearly Report";
            }
            return View(sspartlist);
        }



        public ActionResult IdeaReport()
        {
            return View(context.Ideas.ToList());
        }

        public ActionResult IdeaReportpdf(int id)
        {
            int A = id;
            return new Rotativa.ActionAsPdf("PdfideaReport", new { id = A });
        }

        public ActionResult PdfideaReport(int id)
        {
            var date = DateTime.Now.AddMonths(-id);
            var idealist = context.Ideas.Where(a => a.SubmissionDate > date).ToList();
           
            if (id == 1)
            {
                ViewBag.type = "Idea submissions Monthly Report";
            }
            else if (id == 6)
            {
                ViewBag.type = "Idea submission HalfYear Report";
            }
            else if (id == 12)
            {
                ViewBag.type = "Idea submission Yearly Report";
            }
            return View(idealist);
        }
    }
}
 