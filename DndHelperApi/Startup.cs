﻿using DndHelperApiDal.Configurations;
using DndHelperApiDal.Repositories;
using DndHelperApiDal.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Swagger;

namespace DndHelperApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Dnd Helper Api",
                    Description = "A Api that contains Dnd table data",
                    Contact = new Contact() { Name = "Samuel Spencer", Email = "spencersa715@gmail.com", Url = "https://github.com/spencersa/DndHelperApi" }
                });
            });
            services.AddOptions();
            services.Configure<ConnectionConfiguration>(Configuration.GetSection("ConnectionConfiguration"));
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<IDocumentService, DocumentService>();
            services.AddSingleton<IMongoClient, MongoClient>(client => new MongoClient(Configuration.GetSection("ConnectionConfiguration:DndHelperMongoDbConnectionString").Value));
            services.AddSingleton<IMongoDbRepo, MongoDbRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(
                options => options.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
            );

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
