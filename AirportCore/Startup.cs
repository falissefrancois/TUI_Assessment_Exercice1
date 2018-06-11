
using System.Globalization;
using System.IO;
using AutoMapper;
using BusinessLogic.Data;
using BusinessLogic.Managers;
using BusinessLogic.Managers.Interfaces;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace AirportCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFlightsManager, FlightsManager>();
            services.AddSingleton<IDatabase, Database>();
            services.AddMvc();
            //services.AddAutoMapper();

            //Automapper
            services.AddSingleton(
                new MapperConfiguration(
                    cfg =>
                    {
                        cfg.AddProfile(new MapperProfile());
                    }).CreateMapper());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Exception & statusCodes Page
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
            }

            //Culture set to invariant
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            //Static Files (wwwroot)
            app.UseStaticFiles();

            //Node modules
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = new PathString("/node_modules")
            });

            //Routing
            app.UseMvc((IRouteBuilder routeBuilder) =>
            {
                routeBuilder.MapRoute("Default", "{Controller=Home}/{Action=Index}");
            });
        }
    }
}
