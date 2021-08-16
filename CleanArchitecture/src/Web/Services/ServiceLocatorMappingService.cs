using System;
using Microsoft.Extensions.DependencyInjection;
using Core.Interfaces;


namespace Web.Services
{
    public class ServiceLocatorMappingService : IMapperService
    {
        private readonly IServiceProvider _serviceProvider;
        public ServiceLocatorMappingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        public TDestination Map<TSource, TDestination>(TSource entity)
        {
            var mapper = _serviceProvider.GetService<IMapper<TSource, TDestination>>();

            if (mapper == null)
            {
                throw new Exceptions.MapperNotFoundException(typeof(TSource), typeof(TDestination));
            }

            return mapper.Map(entity);
        }
    }
}
