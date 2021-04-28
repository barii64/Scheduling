﻿using Microsoft.EntityFrameworkCore;
using Scheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.Domain
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<TimerHistory> TimerHistories { get; set; }
        public DbSet<UserTimerHistory> UserTimerHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1321313,
                Email = "admin@gmail.com",
                Password = "5dj3bhWCfxuHmONkBdvFrA==",
                Name = "Admin",
                Surname = "Adminov",
                Position = "lol",
                Department = "Memes",
                Salt = "91ed90df-3289-4fdf-a927-024b24bea8b7",
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 13213133,
                Email = "user@gmail.com",
                Password = "u9DAYiHl+liIqRMvuuciBA==",
                Name = "User",
                Surname = "Userov",
                Position = "lol",
                Department = "Memes",
                Salt = "f0e30e73-fac3-4182-8641-ecba862fed69",
            });

            modelBuilder.Entity<Permission>().HasData(new Permission { 
                Id = 1,
                Name = "canManageUsers"
            });

            modelBuilder.Entity<Permission>().HasData(new Permission { 
                Id = 2,
                Name = "isPartTime"
            });
            
            modelBuilder.Entity<UserPermission>().HasData(new UserPermission
            { 
                Id = 3,
                PermisionId = 1,
                UserId = 1321313
            });

            modelBuilder.Entity<UserPermission>().HasData(new UserPermission
            { 
                Id = 4,
                PermisionId = 2,
                UserId = 13213133
            });
            
            modelBuilder.Entity<UserPermission>().HasData(new UserPermission
            { 
                Id = 5,
                PermisionId = 2,
                UserId = 1321313
            });
            modelBuilder.Entity<TimerHistory>().HasData(new TimerHistory
            {
                Id = 1,
                StartTime = new DateTime(2021, 1, 1, 1, 1, 1),
                FinishTime = new DateTime(2021, 1, 1, 1, 1, 2)
            });

            modelBuilder.Entity<UserTimerHistory>().HasData(new UserTimerHistory
            {
                Id = 1,
                TimerHistoryId = 1,
                UserId = 1321313
            });



        }
    }
}
