using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ContactServiceSolution.Data.Entity;
using ContactServiceSolution.Data.Repositories;
using ContactServiceSolution.Service;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ContactServiceSolution.API.ActionFilters;
using ContactServiceSolution.API.ExceptionMiddleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;

namespace ContactServiceSolution.API
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
            services.AddMvc()
                .AddMvcOptions(Options=> {
                    Options.Filters.Add(new ModelValidator());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<ContactDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ContactDBConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddAutoMapper();
            Mapper.Initialize(cfg => cfg.AddProfile<MappingEntities>());

            services.AddScoped<IContact, ContactService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Contact Service API", Version = "Version 0.1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseHttpsRedirection();
           
            app.UseMvc();
            app.UseSwagger();

            // Serves the Swagger UI
            app.UseSwaggerUI(c =>
            {
                // specifying the Swagger JSON endpoint.
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact Service API");
            });
        }
    }
}
