using Microsoft.EntityFrameworkCore;
using request_scheduler.Domain.MauticForms.Models;
using request_scheduler.Generics.Http.Enums;

namespace request_scheduler.Data.Context
{
    public class RequestSchedulerContext : DbContext
    {
        public DbSet<MauticForm> MauticFormRequests { get; set; }

        public RequestSchedulerContext(DbContextOptions<RequestSchedulerContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MauticForm>()
                .Property(p => p.HttpMethod).HasDefaultValue(HttpMethod.Post);
        }
    }
}
