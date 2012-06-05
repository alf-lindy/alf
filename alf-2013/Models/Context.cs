using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace alf_2013.Models
{
    public class WorkshopContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class WorkshopInitializer : DropCreateDatabaseIfModelChanges<WorkshopContext>
    {
        protected override void Seed(WorkshopContext context)
        {
            var teachers = new List<Teacher> {
                new Teacher { FirstName = "Test", LastName = "Testesen" }
            };
            teachers.ForEach(t => context.Teachers.Add(t));
            context.SaveChanges();
        }
    }
}