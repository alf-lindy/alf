using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Alf.Models
{
    public class RegistrationContext : DbContext
    {
        public DbSet<Participant> Participants { get; set; }
        public DbSet<DanceClass> Classes { get; set; }

        public RegistrationContext() : base("RegistrationContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RegistrationContext, Configuration>());
        }
    }
}