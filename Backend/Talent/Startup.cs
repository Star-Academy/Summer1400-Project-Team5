using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talent.Data;
using Talent.Data.Entities;
using Talent.Services.Interfaces;
using Talent.Services.Repositories;
using Talent.Logic;

namespace Talent
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
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options => 
            {
                string dbId = Environment.GetEnvironmentVariable("DB_ID");
                string dbPs = Environment.GetEnvironmentVariable("DB_PS");
                string connectionString = string.Format(
                    Configuration.GetConnectionString("localConnection"),
                    dbId, dbPs
                );

                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<TokenGenerator, TokenGenerator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseCors(options => 
            {
                options.AllowAnyOrigin().AllowAnyHeader();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
