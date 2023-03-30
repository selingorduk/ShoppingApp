using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoopingApp.DataAccess.Abstract;
using ShoopingApp.DataAccess.Concrete.EFCore;
using ShoopingApp.DataAccess.Concrete.Memory;
using ShopApp.Business.Concrete;
using ShoppingApp.Business.Abstract;
using ShoppingApp.WebUI.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApp.WebUI
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
            services.AddScoped<IProductDAL, EFCoreProductDAL>(); //MemoryProductDAL
            services.AddScoped<IProductService, ProductManager>(); //birbirinden ba��ms�z katmanlar olu�turulur. T�m katmanlar interface �zerinden i�lem yap�yor.

            services.AddMvc().
                SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
            services.AddMvc(options => options.EnableEndpointRouting = false); //error al�nd��� i�in eklendi.
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) //user hatay� g�rmemesi i�in de�i�tirilecek.
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed(); //sadece geli�tirme a�amas�nda kullan�lmas� gereken bir method.
            }
            app.UseStaticFiles();
            app.CustomStaticFiles();
            app.UseMvcWithDefaultRoute();
            {

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }
        }
    }
}
