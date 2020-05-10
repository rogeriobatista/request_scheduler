using System;
using Microsoft.EntityFrameworkCore;
using request_scheduler.Domain.MauticForms.Models;

namespace request_scheduler.Data.Context
{
    public class RequestSchedulerContext : DbContext
    {
        public RequestSchedulerContext(DbContextOptions<RequestSchedulerContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        public DbSet<MauticForm> MauticFormRequests { get; set; }
    }
}
