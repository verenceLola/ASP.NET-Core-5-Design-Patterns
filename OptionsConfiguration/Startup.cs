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
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace OptionsConfiguration
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OptionsConfiguration", Version = "v1" });
            });
            services.Configure<ConfigureMeOptions>(Configuration.GetSection("configureMe"));
            services.AddSingleton<IConfigureOptions<ConfigureMeOptions>, Configure1ConfigureMeOptions>();
            services.AddSingleton<IConfigureOptions<ConfigureMeOptions>, Configure2ConfigureMeOptions>();
            services.Configure<ConfigureMeOptions>(options =>
            {
                options.Lines = options.Lines.Append("Another configure call!"); ;
            });
            services.PostConfigure<ConfigureMeOptions>(options =>
            {
                options.Lines = options.Lines.Append("What about Postconfigure?");
            });
            services.PostConfigureAll<ConfigureMeOptions>(options =>
            {
                options.Lines = options.Lines.Append("Did you forgot about PostConfigureAll?");
            });
            services.ConfigureAll<ConfigureMeOptions>(options =>
            {
                options.Lines = options.Lines.Append("Or ConfigureAll?");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OptionsConfiguration v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/configure-me", async context =>
                {
                    var options = context.RequestServices.GetRequiredService<IOptionsMonitor<ConfigureMeOptions>>();
                    var json = JsonSerializer.Serialize(options, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(json);
                });
            });
        }
    }
}
