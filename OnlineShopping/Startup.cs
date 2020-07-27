using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShopping.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.DataAccess.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using OnlineShopping.Buisness.Interfaces;
using OnlineShopping.Buisness.ManagerClasses;
using OnlineShopping.DataAccess.Repository.Interfaces;

namespace OnlineShopping
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OnlineShoppingContext>(x => x.UseSqlServer(Configuration["ConnectionString:ShoppingDB"]));
            services.AddControllers();
            services.AddCors();
            services.AddScoped<IAuthManager, AuthManager>();
            //services.AddScoped<IAuthRepository<Customers>, AuthRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(opttions =>
                 {
                  opttions.TokenValidationParameters = new TokenValidationParameters
                  {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Configuration.GetSection("AppSettings:SecretKey").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                   };
                  });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllers();
            });
        }
    }
}
