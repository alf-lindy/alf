using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Alf.Models
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Mail { get; set; }
        public Role Role { get; set; }
        public Track Track { get; set; }
        public Level Level { get; set; }
        public bool NTNUI { get; set; }
        public bool Paid { get; set; }
    }

    public enum Role { 
        Lead,
        Follow
    }

    public enum Track { 
        Lindy_Hop,
        Boogie_Woogie
    }

    public enum Level { 
        Beginner,
        Intermediate,
        Advanced
    }

    public class RegistrationContext : DbContext
    {
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RegistrationContext, Configuration>());
        }
    }
}