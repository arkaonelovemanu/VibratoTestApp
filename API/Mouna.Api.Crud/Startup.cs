﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mouna.Api.Crud.BusinessLogic.Interfaces;
using Mouna.Api.Crud.BusinessLogic.Services;
using Mouna.Api.Crud.DataAccess.Interfaces;
using Mouna.Api.Crud.DataAccess.Repository;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Mouna.Api.Crud
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger logger;
        public Startup(IConfiguration config ,ILogger<Startup> log)
        {
            Configuration = config;
            logger = log;


        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEmployeeService, EmployeeService>();
            logger.LogInformation("Added EmployeeService to controller");
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            logger.LogInformation("Added EmployeeRepository to services");
            services.AddCors();
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
           // services.AddTransient<IDbConnection>(sp => new SqlConnection("Server=sqldb;Database=master;User=SA;Password=Welcome@1SA;"));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Mouna CRUD .NET Core API",
                    Description = "My First ASP.NET Core 2.0 Web API to test docker",
                    TermsOfService = "None"

                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", " Mouna CRUD .NET Core API");
            });
            app.UseExceptionHandler(configure =>
            {
                configure.Run(async context =>
                {
                    var ex = context.Features
                                    .Get<IExceptionHandlerFeature>()
                                    .Error;

                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync($"{ex.Message}");
                });
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}