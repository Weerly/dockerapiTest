using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using dockerapi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;


namespace dockerapi
{
#pragma warning disable CS1591
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
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                   ?? "host=localhost;port=5432;database=blogdb;username=bloguser;password=bloguser";
            services.AddDbContext<ApiDbContext>(options =>
                options.UseNpgsql(
                    connectionString
                )
            );

            // var configuration = new Configuration();
            // configuration.TargetDatabase = new DbConnectionInfo(connectionString);
            //
            // var migrator = new DbMigrator(configuration);
            // migrator.Update();

            //var migrator = new DbMigrator(configuration);
            //migrator.Update();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info
                    {
                        Title = "Web API",
                        Version = "v1",
                        Description = "ASP.NET Core Web API with Docker and PostgreSql",
                        TermsOfService = "None",
                        Contact = new Contact
                        {
                            Name = "John Smith",
                        },
                    });
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
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

            app.UseSwagger(o =>
                o.RouteTemplate = "docs/{documentName}/docs.json"
            );

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "docs";
                c.DocumentTitle = "My Swagger UI";
                c.SwaggerEndpoint("/docs/v1/docs.json", "Web API V1");
            });
            app.UseMvc();
        }
    }
#pragma warning restore CS1591
}
