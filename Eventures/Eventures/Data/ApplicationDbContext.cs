﻿using Eventures.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Eventures.Models;

namespace Eventures.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User>  Users { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-D1G8VKM\SQLEXPRESS;Database=Eventures;Integrated Security=true;");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
