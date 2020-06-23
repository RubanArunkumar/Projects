using AirportData.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AirportData
{
    public class AirportDbContext:DbContext
    {
        public AirportDbContext(DbContextOptions<AirportDbContext> options) : base(options)
        {
        }

        public virtual DbSet<AirportDetails> AirportDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
