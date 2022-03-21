﻿using Reports.DAL.Models;

namespace Reports.Server.DbContexts
{
    public sealed class ReportsDbContext : DbContext
    {
        public ReportsDbContext(DbContextOptions<ReportsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        
        public DbSet<TaskChangeModel> Changes { get; set; }
        
        public DbSet<TaskCommentModel> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>().HasOne(e => e.Boss);
            modelBuilder.Entity<TaskModel>().HasOne(t => t.Executor);
            modelBuilder.Entity<TaskChangeModel>().HasOne(change => change.Task);
            modelBuilder.Entity<TaskChangeModel>().HasOne(change => change.Employee);
            modelBuilder.Entity<TaskCommentModel>().HasOne(comment => comment.ChangeInfo);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}