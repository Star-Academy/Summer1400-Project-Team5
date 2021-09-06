using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication2", Version = "v1" });
            });
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

            var singletonSqlHandler =
                new SqlHandler(new SqlConnection(Configuration.GetConnectionString("testConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ISqlTable, SqlTable>();
            services.AddSingleton<ICsvToTable, CsvToTable>();
            services.AddSingleton(typeof(ISqlHandler), singletonSqlHandler);
            services.AddSingleton(typeof(SqlHandler), singletonSqlHandler);
            services.AddSingleton<ICsvParser, CsvParser>();
            services.AddSingleton<ISqlParser, SqlParser>();
            services.AddSingleton<ICsvDownloader, CsvDownloader>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication2 v1"));
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
