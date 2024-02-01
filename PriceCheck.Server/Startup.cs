using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PriceCheck.API.Infrastructure.Extensions;
using PriceCheck.BusinessLogic;
using PriceCheck.BusinessLogic.Interfaces;
using PriceCheck.BusinessLogic.Services;
using PriceCheck.Data;
using PriceCheck.Data.Interfaces;
using PriceCheck.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;


namespace PriceCheck.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void AddServices(IServiceCollection services)
        {
            services.AddDbContext<PriceCheckContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers();

            services.AddScoped<IRepository, EFCoreRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();

            services.AddScoped<IATBService ,ATBService>();

            services.AddScoped <ILinkValidator, LinkValidator>();
            services.AddScoped<ICrawlerService, ATBCrawlerService>();
            services.AddScoped<ITransliterationService, TransliterationService>();

            services.AddHttpClient<ICrawlerService, ATBCrawlerService>();
            services.AddHttpClient<IParserService, ATBParserService>();

            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddAutoMapper(typeof(BusinessLogicAssemblyMarker));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseExceptionHandling();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDbTransaction();

            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
                 endpoints.MapFallbackToFile("/index.html");
             });                   

        }
    }
}
