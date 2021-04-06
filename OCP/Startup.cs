using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace OCP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        { }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";

                var target = new Ninja("The Unseen Mirage");
                var ninja = new Ninja("The Blue Phantom");

                ninja.EquippedWeapon = new Sword();
                var result = ninja.Attack(target);
                await PrintAttackResult(result);

                ninja.EquippedWeapon = new Shuriken();
                var result2 = ninja.Attack(target);
                await PrintAttackResult(result2);

                async Task PrintAttackResult(AttackResult attackResult)
                {
                    await context.Response.WriteAsync($"'{attackResult.Attacker}' attacked '{attackResult.Target}' using '{attackResult.Weapon.GetType().Name}'!<br>");
                }
            });
        }
    }
}
