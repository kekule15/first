using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using first.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace first.data
{
    public class DataContextEF : DbContext

    {
        private readonly IConfiguration? _config;
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<Computer>? Computer { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // if the DB is not yet configured , run the configuration 
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config!.GetConnectionString("DefaultConnection")!, optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // if you provide a schema and Entity Framwork matches a table from the connection string and the model provided, 
            // No need to explicitely declare the table name and schema name.
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>().HasKey(c => c.ComputerId);
            //.ToTable("Computer", "TutorialAppSchema");
            // .ToTable("TableName", "SchemaName");

            base.OnModelCreating(modelBuilder);
        }
    }


}