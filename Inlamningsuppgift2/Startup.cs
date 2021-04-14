using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Inlamningsuppgift2.Models;


namespace Inlamningsuppgift2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);

            //Connectionsträngen bör egentligen ligga I en config fil men vi tittar mer
            //på det senare

            //var conn = @"Server=localhost\SQL15;Database=Tomasos;uid=sa; pwd=123;Trusted_Connection=true;Integrated Security=false;";
            //var conn = @"Server=localhost\SQL15;Database=Tomasos;Trusted_Connection=True; ConnectRetryCount=0";
            var conn = @"Server=localhost;Database=Tomasos;Trusted_Connection=True; ConnectRetryCount=0";
           

            services.AddDbContext<TomasosContext>(options => options.UseSqlServer(conn));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            //app.UseSession();

            app.UseRouting();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //    name: "Default",
            //    template: "{controller=Home}/{action=Index}"
            //        );

            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

              

            });
        }
    }
}
