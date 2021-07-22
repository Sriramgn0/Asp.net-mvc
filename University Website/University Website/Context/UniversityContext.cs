using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using University_Website.Models;

namespace University_Website.Context
{
    public class UniversityContext:DbContext
    {
        public DbSet<UserRegistration> UserRegistrations { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Club> Clubs { get; set; }

        public DbSet<ClubMember> ClubMembers { get; set; }

        public DbSet<DepartmentType> DepartmentTypes { get; set; }

        public DbSet<EventParticipants> EventParticipantss { get; set; }


       public DbSet<Events> Eventss { get; set; }

        public DbSet<Grievances> Grievancess { get; set; }

        public DbSet<Idea> Ideas { get; set; }

        public DbSet<IdeaReviewers> IdeaReviewerss { get; set; }

        public DbSet<Issue>Issues { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Posts> Posts { get; set; }

        public DbSet<SocialService> SocialServices { get; set; }

        public DbSet<SocialServiceParticipants> SocialServiceParticipantss { get; set; }

        public System.Data.Entity.DbSet<University_Website.Models.ideawithnames> ideawithnames { get; set; }
    }
}