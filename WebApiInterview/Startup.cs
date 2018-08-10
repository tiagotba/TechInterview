using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiInterview.Context;
using WebApiInterview.Services.Infrastructure;
using WebApiInterview.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebApiInterview
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
            // Add framework services.
            // services.AddDbContext<Interview_Context>(opt => opt.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0"));
            services.AddDbContext<Interview_Context>(options =>
                 options.UseInMemoryDatabase("InMemoryDatabase"));
            services.AddMvc().AddControllersAsServices();
            services.AddTransient<IArticles, ArticleRepository>();
            services.AddTransient<ICarts, CartRepository>();
            services.AddTransient<IDiscounts, DiscountRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseMvc();
        }
    }
}
