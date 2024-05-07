using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Interview.Web.Extensions;
using Interview.Web.Filters;
using Microsoft.EntityFrameworkCore;
using Sparcpoint;
using Sparcpoint.Infrastructure;
using Sparcpoint.Infrastructure.Implementations;

namespace Interview.Web
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
            services.AddControllers(options =>
            {
                options.Filters.Add(new GeneralExceptionFilter());
                options.Filters.Add(new ModelValidationFilter());
            });
            services.AddSwaggerGen(o =>
            {
                o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Interview.Web.xml"));
            });
            services.AddDbContext<ProductContext>(builder =>
            {
                //I might have used some other DB with docker, but this one doesn't require much overhead
                builder.UseSqlite(Configuration.GetConnectionString("Products"));
            });
            services.AddScoped<IProductRepository, ProductRepository>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.InitializeDb();
        }
    }
}
