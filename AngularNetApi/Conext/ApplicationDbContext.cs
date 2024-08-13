﻿using AngularNetApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AngularNetApi.Conext
{
    public class ApplicationDbContext : IdentityDbContext<UserCredentials>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserRegistry> UserRegistries { get; set; }
        public DbSet<CompanyRegistry> CompanyRegistries { get; set; }
    }
}