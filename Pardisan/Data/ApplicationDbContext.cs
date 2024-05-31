using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pardisan.Models;
using Pardisan.Models.Blog;
using Pardisan.Models.News;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pardisan.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }

        public DbSet<Blog> Blog { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Image> Images { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}


    }
}
