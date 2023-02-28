using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HFT.Logic.Interfaces;
using HFT.Logic.Services;
using HFT.Models;
using HFT.Repository;
using HFT.Repository.Interfaces;
using HFT.Repository.Repositories;

namespace HFT.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CarDbContext>();

            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IOwnerRepository, OwnerRepository>();

            services.AddTransient<IBrandLogic, BrandLogic>();
            services.AddTransient<ICarLogic, CarLogic>();
            services.AddTransient<IOwnerLogic, OwnerLogic>();

            services.AddControllers();
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
                endpoints.MapControllers();
            });
        }
    }
}
