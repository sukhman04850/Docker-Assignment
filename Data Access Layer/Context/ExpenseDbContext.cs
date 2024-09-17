using Data_Access_Layer.Hasher;
using Microsoft.EntityFrameworkCore;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Context
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options)
        {

        }

        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<ExpenseGroup> ExpenseGroup { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ExpenseShare> ExpenseShare { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ExpenseGroup>()
                .HasKey(eg => eg.GroupId);

            modelBuilder.Entity<ExpenseGroup>()
                .HasKey(eg => eg.GroupId);

            modelBuilder.Entity<Users>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<ExpenseShare>()
                .HasKey(es => es.Id);
          

            modelBuilder.Entity<ExpenseGroup>()
                .HasMany(eg => eg.Users)
                .WithMany(u => u.ExpenseGroups)
                .UsingEntity<Dictionary<string, object>>(
                    "UserExpenseGroup",
                    j => j.HasOne<Users>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<ExpenseGroup>().WithMany().HasForeignKey("GroupId"));

            modelBuilder.Entity<ExpenseGroup>()
                .HasMany(eg => eg.Expenses)
                .WithOne(e => e.ExpenseGroup)
                .HasForeignKey(e => e.ExpenseGroupId);

            modelBuilder.Entity<Expenses>()
                .HasMany(e => e.ExpenseShares)
                .WithOne(es => es.Expense)
                .HasForeignKey(es => es.ExpenseId);

            modelBuilder.Entity<Expenses>()
                .HasOne(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserId);

            string password = PasswordHasher.HashPassword("Admin@123");

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = Guid.NewGuid(),
                    Name = "Pratham",
                    Email = "pratham@mail.com",
                    Password = password,
                    IsAdmin = false
                },
                new Users
                {
                    Id = Guid.NewGuid(),
                    Name = "Madhur",
                    Email = "madhur@mail.com",
                    Password = password,
                    IsAdmin = false
                },
                new Users
                {
                    Id = Guid.NewGuid(),
                    Name = "Sukhman",
                    Email = "sukhman@mail.com",
                    Password = password,
                    IsAdmin = true
                },
                new Users
                {
                    Id=Guid.NewGuid(),
                    Name="Swapnil",
                    Email="swapnil@mail.com",
                    Password = password,
                    IsAdmin=false
                },
                new Users
                {
                    Id=Guid.NewGuid(),
                    Name="Shivansh",
                    Email="shivansh@mail.com",
                    Password = password,
                    IsAdmin=false
                },
                new Users
                {
                    Id = Guid.NewGuid(),
                    Name = "Rishav",
                    Email = "rishhav@mail.com",
                    Password = password,
                    IsAdmin = false
                },
                new Users
                {
                    Id= Guid.NewGuid(),
                    Name="Ryan",
                    Email="ryan@mail.com",
                    Password = password,
                    IsAdmin = false
                }
            );
        }
    }
}
