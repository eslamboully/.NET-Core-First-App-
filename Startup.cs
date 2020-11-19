using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Bookstore.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Bookstore.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Bookstore
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IBookstoreRepository<Author>, AuthorDbRepository>();
            services.AddScoped<IBookstoreRepository<Book>, BookDbRepository>();

            services.AddDbContext<BookstoreDbContext>(options => 
            {
                options.UseMySql(configuration.GetConnectionString("MySql"),ServerVersion.AutoDetect(configuration.GetConnectionString("MySql")));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseStaticFiles(); 
        }
    }
}