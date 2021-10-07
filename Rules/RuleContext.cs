using Microsoft.EntityFrameworkCore;
using System;

namespace Rules
{
    public class RuleContext : DbContext, IRuleContext
    {
        public DbSet<Rule> Rules { get; set; }

        public string DbPath { get; private set; }

        public RuleContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}Rules.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add your own configuration here
            modelBuilder.Entity<Rule>().HasData(
            new Rule() { Id = 1, Code = "LIVE", Description = "Live", Multiple = 3, ResultField = "Description" },
            new Rule() { Id = 2, Code = "NATION", Description = "Nation", Multiple = 5, ResultField = "Description" },
            new Rule() { Id = 3, Code = "DEFAULT", Description = "Integer", Multiple = 0, ResultField = "Value" });
        }
    }
}