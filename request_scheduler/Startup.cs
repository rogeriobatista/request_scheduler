using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using request_scheduler.Data.Context;
using request_scheduler.Data.Repositories;
using request_scheduler.Domain.MauticForms.Enums;
using request_scheduler.Domain.MauticForms.Interfaces;
using request_scheduler.Domain.MauticForms.Services;
using request_scheduler.Queues.Consumers;
using request_scheduler.Queues.Producers;

namespace request_scheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(ISendMauticFormProducer), typeof(SendMauticFormProducer));
            services.AddScoped(typeof(IMauticFormRepository), typeof(MauticFormRepository));
            services.AddScoped(typeof(IMauticFormService), typeof(MauticFormService));
            services.AddScoped(typeof(ISendMauticFormConsumer), typeof(SendMauticFormConsumer));

            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UsePostgreSqlStorage(Configuration.GetConnectionString("PostgreSql"))
                );
            services.AddHangfireServer();
            services.AddControllers();
            services.AddDbContext<RequestSchedulerContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("PostgreSql")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IRecurringJobManager recurringJobManager,
            IMauticFormService mauticFormService,
            ISendMauticFormConsumer sendMauticFormConsumer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            sendMauticFormConsumer.Register();

            app.UseHangfireDashboard();

            recurringJobManager.AddOrUpdate(
                "Send Mautic Form Post Each Minute",
                () => mauticFormService.Enqueue(MauticFormSendFrequency.Minutely, 10),
                Cron.Minutely()
                );

            recurringJobManager.AddOrUpdate(
                "Send Mautic Form Post Each 10 Minutes",
                () => mauticFormService.Enqueue(MauticFormSendFrequency.Each10Minutes, 20),
                Cron.MinuteInterval(10)
            );
        }
    }
}
