using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using AnstigramAPI.Interfaces;
using AnstigramAPI.Repositories;
using AnstigramAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AnstigramAPI
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
            //https
            

            //accestoken 


            //Dbcontext
            string sqlServer = @"Data Source=LAPTOP-TLNR6N6N\SQLEXPRESS; Initial Catalog=Anstigram; Integrated Security=True; Connection Timeout=5;";
            services.AddDbContext<AccountContext>(options => options.UseSqlServer(sqlServer));

            //Repository pattern dependicy injection
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            //Allow request from different domains.
            services.AddCors(c => {
                c.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());
                c.AddPolicy("myPolicy", builder => builder.WithOrigins("localhost:3000"));
            });
            //JSON serializer
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options=>
                options.SerializerSettings.ReferenceLoopHandling=Newtonsoft
                .Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                = new DefaultContractResolver());
            

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnstigramAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnstigramAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
