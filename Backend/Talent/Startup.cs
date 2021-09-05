using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Talent.Data;
using Talent.Data.Entities;
using Talent.Models;
using Talent.Models.DatabaseModels;
using Talent.Services.Interfaces;
using Talent.Services.Parsers;
using Talent.Services.Repositories;

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
                options.UseSqlServer(
                    Configuration.GetConnectionString("testConnection")
                );
            });
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ISqlTable, SqlTable>();
            services.AddSingleton<ICsvToTable, CsvToTable>();
            services.AddSingleton(typeof(SqlConnection),
             new SqlConnection(Configuration.GetConnectionString("testConnection")));
            services.AddSingleton<ISqlHandler, SqlHandler>();
            services.AddSingleton<ICsvParser, CsvParser>();
            services.AddSingleton<ISqlParser, SqlParser>();
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
                options.AllowAnyOrigin();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
