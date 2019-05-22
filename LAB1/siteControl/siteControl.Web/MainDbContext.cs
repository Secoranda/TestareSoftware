using siteControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace siteControl.Web
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(): base ("name=db")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MainDbContext>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Truck> Trucks { get; set; }

    }

}