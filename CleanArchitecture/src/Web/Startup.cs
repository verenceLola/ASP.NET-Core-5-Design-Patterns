using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Core.UseCases;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ForEvolve.EntityFrameworkCore.Seeders;
using ForEvolve.DependencyInjection;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Core.Interfaces;


namespace Web
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
            services.AddDependencyInjectionModules(typeof(Startup).Assembly);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Layering", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            app.Seed<ProductContext>();
        }
    }
    public class DomainLayerModule : DependencyInjectionModule
    {
        public DomainLayerModule(IServiceCollection services)
            : base(services)
        {
            services.AddScoped<AddStocks>();
            services.AddScoped<RemoveStocks>();
        }
    }

    public class DataLayerModule : DependencyInjectionModule
    {
        public DataLayerModule(IServiceCollection services)
            : base(services)
        {
            services.AddAutoMapper(
                GetType().Assembly,
                typeof(Infrastructure.Data.Mappers.ProductProfile).Assembly
            );
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddDbContext<ProductContext>(options => options
                .UseInMemoryDatabase("ProductContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            );
            services.AddForEvolveSeeders().Scan<Startup>();
        }
    }

    public class ProductSeeder : ISeeder<ProductContext>
    {
        public void Seed(ProductContext db)
        {
            db.Products.Add(new()
            {
                Id = 1,
                Name = "Banana",
                QuantityInStock = 50
            });
            db.Products.Add(new()
            {
                Id = 2,
                Name = "Apple",
                QuantityInStock = 20
            });
            db.Products.Add(new()
            {
                Id = 3,
                Name = "Habanero Pepper",
                QuantityInStock = 10
            });
            db.SaveChanges();
        }
    }
}
