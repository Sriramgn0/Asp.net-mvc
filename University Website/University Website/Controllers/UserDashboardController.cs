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
    public class UserDashboardController : Controller
    {
        UniversityContext context = new UniversityContext();
        public ActionResult DashBoard()
        {
            if (Session["userid"] != null)
            {
                ViewBag.UserID = Session["userid"];
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserRegistration");
            }
        }
        public ActionResult SubmitGrievances(int UserId)
        {
            ViewBag.UserID = Session["userid"];
            return View();
        }
        [HttpPost]
        public ActionResult SubmitGrievances(Grievances obj)
        {
            obj.Status = Grievances.grief.New;
            obj.DeadLine = Convert.ToDateTime("01/01/2020");

            context.Grievancess.Add(obj);
            context.SaveChanges();
            ViewBag.message = "Submitted Successfully!";
            return View();
        }
        public ActionResult CheckGStatus(int UserId)
        {
            var lis = context.Grievancess.Where(a => a.UserID == UserId).ToList();
            var DeadLine = Convert.ToDateTime("01/01/2020");
            ViewBag.Date = DeadLine;
            ViewBag.message = "Will be updated soon";
            return View(lis);


        }
        public ActionResult SubmitIssue()
        {
            ViewBag.UserID = Session["userid"];
            return View();

        }
        [HttpPost]
        public ActionResult SubmitIssue(Issue obj1)
        {

            obj1.IssueDate = Convert.ToDateTime(DateTime.Now);

            context.Issues.Add(obj1);
            context.SaveChanges();
            ViewBag.message = "Submitted Successfully!";
            return View();
        }
        
        public ActionResult CheckIStatus(int UserId)
        {
            var lis1 = context.Issues.Where(a => a.UserID == UserId).ToList();
            //var DeadLine = Convert.ToDateTime("01/01/2020");
            //ViewBag.Date = DeadLine;
            //ViewBag.message = "Will be updated soon";
            return View(lis1);


        }
        public ActionResult logout(int id)
        {
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();
            return RedirectToAction("Index", "UserRegistration");
        }

      

        public ActionResult AddIdea()
        {
            if (Session["userid"] != null)
            {
                List<Club> clublist = context.Clubs.ToList();
                ViewBag.ClubList = new SelectList(clublist, "ClubId", "ClubName");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserRegistration");
            }
        }

        public JsonResult GetCategory(int Clubid)
        {
            List<Category> CategoryList = context.Categories.Where(x => x.ClubId == Clubid).ToList();
            return Json(CategoryList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddIdea(Idea obj)
        {
            if(obj.IdeaTitle!=null)
            {
                obj.NoOFDevote = 0;
                obj.NoOfVotes = 0;
                obj.SubmissionDate = DateTime.Now;
                obj.Status = Idea.stat.New;
                obj.UserId = Convert.ToInt32(Session["userid"]);
               
                context.Ideas.Add(obj);
                context.SaveChanges();
                ViewBag.Message = "Your Idea submitted Successfully";
                List<Club> clublist = context.Clubs.ToList();
                ViewBag.ClubList = new SelectList(clublist, "ClubId", "ClubName");
                return View();
            }
            else
            {
                ViewBag.Message1 = "Please Add details properly";
                return View();
            }
        }



        public ActionResult ViewClubs()
        {
            return View(context.Clubs.ToList());
        }

        public ActionResult JoinClub(int id)
        {
            var UserID = Session["userid"];
            var userdata = context.UserRegistrations.Find(Convert.ToInt32(UserID));
            var clubdata = context.Clubs.Find(id);
            var age = Convert.ToInt32(DateTime.Now.Year - userdata.DateOfBirth.Year);
            if (Convert.ToInt32(clubdata.EligibleAge) > age)
            {
                ViewBag.Message = "Sorry! Not eligible to join the club!";

                return View();
            }
            else
            {
                var obj = new ClubMember();
                obj.ClubId = id;
                obj.UserId = Convert.ToInt32(UserID);
                ViewBag.Message1 = "Congrats! You are a member now";
                context.ClubMembers.Add(obj);
                context.SaveChanges();
                return View();
            }
        }

        public ActionResult ViewENClubs()
        {
            var userId = Convert.ToInt32(Session["userid"]);
            var ExistingList = (from a in context.ClubMembers where a.UserId == userId select a).ToList();

            return View(ExistingList);
        }

        public ActionResult LeaveClub(int Cid)
        {
            try
            {
                int id = Convert.ToInt32(Session["userid"]);
                var CMid = context.ClubMembers.FirstOrDefault(a => a.UserId == id && a.ClubId == Cid);
                context.ClubMembers.Remove(CMid);
                context.SaveChanges();
                ViewBag.Message = "Left the club successfully!";
                return View();
            }
            catch
            {
                ViewBag.Message = "Not an existing member of this club";
                return View();
            }
        }
        public ActionResult ViewEMembers(int id)
        {

            var userIds = context.ClubMembers.Where(a => a.ClubId == id).ToList();
            var userData = new List<UserRegistration>();
            foreach (var i in userIds)
            {

                var userNames = context.UserRegistrations.Find(i.UserId);
                userData.Add(userNames);
            }
            return View(userData);
        }


       





        public ActionResult UpdateGrievance()
        {
            var SList = new List<string>() { "New", "Completed" };
            ViewBag.StatusList = SList;
            var tempGrie = context.Grievancess.Find(Session["userid"]);
            ViewBag.Grie = tempGrie;
            return View();
        }


        [HttpPost]
        public ActionResult UpdateGrievance(Grievances obj)
        {
            var tempGrie = context.Grievancess.Find(Session["userid"]);
            tempGrie.Status = obj.Status;
            tempGrie.Details = obj.Details;
            tempGrie.Suggestion = obj.Suggestion;
            context.Entry(tempGrie).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Dashboard");

        }

        public ActionResult Eventdashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Eventdashboard(Events obj)
        {
            if((obj.StartDate==null)&&((obj.EndDate==null)||(obj.EventName==null)))
            {
                ViewBag.Message = "Enter Event name or Event id or search between event dates";
                return View();
            }
            else if(obj.EventName != null)
            {
                var eventlist = context.Eventss.ToList();
                var selectedlist = new List<Events>();
                foreach(var i in eventlist)
                {
                    if(i.EventId.ToString().Contains(obj.EventName)||(i.EventName.Contains(obj.EventName)))
                    {
                        selectedlist.Add(i);
                    }
                }
                ViewBag.HasData = "yes";
                ViewBag.events = selectedlist;
                return View();
            }
            else
            {
                if(obj.StartDate > obj.EndDate)
                    {
                      ViewBag.Message = "Start date should be lesser than End date";
                      return View();
                    }
                else
                    {
                       var eventlist = context.Eventss.Where(a => a.StartDate <= obj.StartDate && a.EndDate <= obj.EndDate).ToList();
                       ViewBag.events = eventlist;
                      ViewBag.HasData = "yes";
                      return View();
                    }
            }
        }


        public ActionResult JoinEvent(int id)
        {
            EventParticipants obj = new EventParticipants();
            obj.EventID = id;
            obj.UserID = Convert.ToInt32(Session["userid"]);
            obj.userchoice = EventParticipants.likeordislike.select;
            obj.Attendence = EventParticipants.att.Select;
            obj.Review = EventParticipants.review.Select;
            context.EventParticipantss.Add(obj);
            context.SaveChanges();
            ViewBag.Eventname = context.Eventss.Find(id).EventName;
            ViewBag.Eventdata = obj;
            return View();
        }

        [HttpPost]
        public ActionResult JoinEvent(EventParticipants obj)
        {
            var uid = Convert.ToInt32(Session["userid"]);
            var userdata = context.EventParticipantss.FirstOrDefault(a=>a.UserID==uid&& a.EventID==obj.EventID);
            userdata.UserComment = obj.UserComment;
            userdata.userchoice = obj.userchoice;
            userdata.Review = obj.Review;
            userdata.Attendence = obj.Attendence;
            context.Entry(userdata).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Eventdashboard");
        }

        public ActionResult ViewEventParticipents(int id)
        {
            var userid = context.EventParticipantss.Where(a => a.EventID == id).ToList();
            var userlist = new List<UserRegistration>();
            foreach(var i in userid)
            {
                userlist.Add(context.UserRegistrations.Find(i.UserID));
            }
            return View(userlist);
        }

        public ActionResult ShareEvent(int id,string type)
        {
            List<DepartmentType> dlist = context.DepartmentTypes.ToList();
            ViewBag.UserId = Convert.ToInt32(Session["userid"]);
            ViewBag.deptlist = new SelectList(dlist, "DepartmentId", "Departmenttype");
            ViewBag.EventName = context.Eventss.Find(id).EventName;
            ViewBag.Link = "UserDashBoard/"+type+"/"+id;
            return View();
        }



        public ActionResult ViewIdeas()
        {
            var ideawithna = new List<ideawithnames>();

            foreach(var i in context.Ideas.ToList())
            {
                ideawithnames ob = new ideawithnames();
                ob.ideas = i;
                var userid = i.UserId;
                ob.username = context.UserRegistrations.Find(userid).First_Name;
                ob.vote = "Not Yet";
                ideawithna.Add(ob);
            }
            ViewBag.data = ideawithna;
            return View();
        }

      
        public ActionResult VoteforIdea(int id)
        {
            var idea = context.Ideas.Find(id);

            IdeaReviewers idr = new IdeaReviewers();
            idr.UserID = Convert.ToInt32(Session["userid"]);
            idr.Vote = 1;
            idr.IdeaId = id;
            context.IdeaReviewerss.Add(idr);
            context.SaveChanges();
            return RedirectToAction("ViewIdeas");
        }


        public ActionResult adddepartment()
        {
            return View();
        }

        [HttpPost]

        public ActionResult adddepartment(DepartmentType obj)
        {
            context.DepartmentTypes.Add(obj);
            context.SaveChanges();
            return View();
        }

        public ActionResult ViewSocialServices()
        {

            return View(context.SocialServices.ToList());
        }
        public ActionResult JoinAsVolunteer(int id)
        {
            var PCount = context.SocialServiceParticipantss.Where(a => a.SocialServiceId == id).Count();
            if (PCount == context.SocialServices.Find(id).NoOfVolunteer)
            {
                ViewBag.Message = "No volunteers required";
                return View();
            }
            else
            {
                SocialServiceParticipants obj = new SocialServiceParticipants();
                obj.SocialServiceId = id;
                obj.UserID = Convert.ToInt32(Session["userid"]);
                obj.VolunterOrNot = 1;
                context.SocialServiceParticipantss.Add(obj);
                context.SaveChanges();
                ViewBag.Message = "Successfully registered for this Social Service";
                return View();

            }


        }
        public ActionResult LeaveAsVolunteer(int id)
        {
            try
            {
                var uid = Convert.ToInt32(Session["userid"]);
                var participant = context.SocialServiceParticipantss.FirstOrDefault(a => a.SocialServiceId == id && a.UserID == uid);
                context.SocialServiceParticipantss.Remove(participant);
                context.SaveChanges();
                ViewBag.Message = "Left Social Service successfully!";
                return View();
            }
            catch
            {
                ViewBag.Message = "You are not a volunteer";
                return View();
            }
        }

        public ActionResult ViewVolunteers(int id)
        {
            var userId = context.SocialServiceParticipantss.Where(a => a.SocialServiceId == id).ToList();
            var VList = new List<UserRegistration>();
            foreach (var i in userId)
            {

                var ud1 = context.UserRegistrations.Find(i.UserID);
                VList.Add(ud1);
            }
            return View(VList);
        }


        public ActionResult SearchIdea()
        {
            if (Session["userid"] == null)
            {
                List<Club> clublist = context.Clubs.ToList();
                ViewBag.ClubList = new SelectList(clublist, "ClubId", "ClubName");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "UserRegistration");
            }
        }
        [HttpPost]
        public ActionResult SearchIdea(Idea obj)
        {
            if ((obj.IdeaTitle == null) && (obj.CategoryId==0))
            {
                List<Club> clublist = context.Clubs.ToList();
                ViewBag.ClubList = new SelectList(clublist, "ClubId", "ClubName");
                ViewBag.Message = "Enter Idea name or Select valid Clubs";
                return View();
            }
            else if (obj.IdeaTitle != null)
            {
                var idealist = context.Ideas.ToList();
                var selectedlist = new List<Idea>();
                foreach (var i in idealist)
                {
                    if (i.IdeaTitle.ToString().Contains(obj.IdeaTitle) )
                    {
                        selectedlist.Add(i);
                    }
                }
                ViewBag.HasData = "yes";
                ViewBag.ideas = selectedlist;
                List<Club> clublist = context.Clubs.ToList();
                ViewBag.ClubList = new SelectList(clublist, "ClubId", "ClubName");
                return View();
            }
            else
            {
                var idealist = context.Ideas.Where(a => a.CategoryId == obj.CategoryId).ToList();
                ViewBag.HasData = "yes";
                ViewBag.ideas = idealist;
                List<Club> clublist = context.Clubs.ToList();
                ViewBag.ClubList = new SelectList(clublist, "ClubId", "ClubName");
                return View();
            }
        }

    }
      
}