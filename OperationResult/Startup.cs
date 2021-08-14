using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace OperationResult
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OperationResult", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouter(builder =>
            {
                builder.MapGet("/simplest-form", SimplestFormHandler);
                builder.MapGet("/single-error", SingleErrorHandler);
                builder.MapGet("/single-error-with-value", SingleErrorWithValueHandler);
                builder.MapGet("/multiple-errors-with-value", MultipleErrorsWithValueHandler);
                builder.MapGet("/mutilple-errors-with-value-and-severity", MutipleErrorsWithValueAndSeverityHandler);
            });
        }
        private async Task SimplestFormHandler(HttpRequest request, HttpResponse response, RouteData data)
        {
            var executor = new SimplestForm.Executor();
            var result = executor.Operation();

            if (result.Succeeded)
            {
                await response.WriteAsync("Operation succeeded");
            }
            else
            {
                await response.WriteAsync("Operation failed");
            }
        }
        private async Task SingleErrorHandler(HttpRequest request, HttpResponse response, RouteData data)
        {
            var executor = new SingleError.Executor();
            var result = executor.Operation();

            if (result.Succeeded)
            {
                await response.WriteAsync("Operation succeeded");
            }
            else
            {
                await response.WriteAsync(result.ErrorMessage);
            }
        }

        private async Task SingleErrorWithValueHandler(HttpRequest request, HttpResponse response, RouteData data)
        {
            var executor = new SingleErrorWithValue.Executor();
            var result = executor.Operation();

            if (result.Succeeded)
            {
                await response.WriteAsync($"Operation succeeded with a value of '{result.Value}'.");
            }
            else
            {
                await response.WriteAsync(result.ErrorMessage);
            }
        }

        private async Task MultipleErrorsWithValueHandler(HttpRequest request, HttpResponse response, RouteData data)
        {
            var executor = new MultipleErrorsWithValue.Executor();
            var result = executor.Operation();

            if (result.Succeeded)
            {
                await response.WriteAsync($"Operation succeeded with a value of '{result.Value}'.");
            }
            else
            {
                var json = JsonSerializer.Serialize(result.Errors);
                response.Headers["ContentType"] = "application/json";

                await response.WriteAsync(json);
            }
        }

        private async Task MutipleErrorsWithValueAndSeverityHandler(HttpRequest request, HttpResponse response, RouteData data)
        {
            var executor = new StaticFactoryMethod.Executor();
            var result = executor.Operation();

            if (result.Succeeded)
            {
                await response.WriteAsync($"Operation succeeded for number '{result.Value}.");
            }
            else
            {
                var json = JsonSerializer.Serialize(result);
                response.Headers["ContentType"] = "application/json";
                await response.WriteAsync(json);
            }
        }
    }

}
