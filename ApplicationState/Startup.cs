#define USE_MEMORY_CACHE
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApplicationState.Interfaces;

namespace ApplicationState
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

#if USE_MEMORY_CACHE
            services.AddMemoryCache();
            services.AddSingleton<IApplicationState, ApplicationMemoryCache>();
#else
                services.AddSingleton<IApplicationState, ApplicationDictionary>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApplicationState myAppState)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                if (context.Request.Method == "GET")
                {
                    await HandleGetRequestAsync(myAppState, context);
                }
                else
                {
                    await HandlePostRequestAsync(myAppState, context);
                }
            });
        }
        private static async Task HandleGetRequestAsync(IApplicationState myAppState, HttpContext context)
        {
            var key = context.Request.Query["key"];
            if (key.Count != 1)
            {
                await context.Response.WriteAsync("You must specify a single 'key' parameter like '?key=SomeAppStateKey'.");
                return;
            }
            var value = myAppState.Get<string>(key.Single());
            await context.Response.WriteAsync($"{key} = {value ?? "null"}");
        }
        private static async Task HandlePostRequestAsync(IApplicationState myAppState, HttpContext context)
        {
            var key = context.Request.Form["key"].SingleOrDefault();
            var value = context.Request.Form["Value"].SingleOrDefault();
            if (key == null || value == null)
            {
                await context.Response.WriteAsync("You must specify both a 'key' and a 'value'.");
                return;
            }
            myAppState.Set(key, value);
            await context.Response.WriteAsync($"{key} = {value ?? "null"}");
        }
    }
}
