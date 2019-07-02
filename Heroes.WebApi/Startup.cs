using Heroes.Data;
using Heroes.Data.Models;
using Heroes.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Heroes.WebApi
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
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new Info
                        {
                            Title = "Welcome to the Heroes for Hire REST API",
                            Version = $"v1",
                            Description = "We're glad you're here, and we hope you'll find the documentation helpful.",
                            Contact = new Contact
                            {
                                Name = "Nick Fury",
                                Email = "nickfury@heroesforhire.com"
                            }
                        });

                    c.EnableAnnotations();
                });


            var connectionString = 
                Configuration["SqlServer:ConnectionString"];

            services.AddDbContext<EntitiesDbContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(
                    options, connectionString));

            services.AddTransient<IHeroEntityStore, HeroEntityStore>();
            services.AddTransient<IHeroService, HeroService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Heroes for Hire | API Reference";

                c.DocExpansion(DocExpansion.List);

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Heroes for Hire REST API V1");
            });

            app.UseCors("AllowAllHeaders");
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
