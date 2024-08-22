﻿using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=LAPTOP\\MSSQLSERVER01;database=DentalHealthDb;integrated security=true;TrustServerCertificate=true");
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<HealthGoal> HealthGoals { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Note> Notes { get; set; }

    }
}
