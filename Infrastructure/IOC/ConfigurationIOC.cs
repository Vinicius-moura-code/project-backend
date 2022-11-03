using Application;
using Application.Interfaces;
using Application.Mapper;
using Autofac;
using AutoMapper;
using Domain.Core.Interfaces.Repositories;
using Domain.Core.Interfaces.Services;
using Domain.Services;
using Infrastructure.DATA;

namespace Infrastructure.IOC
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppLogin>().As<IAppLogin>();
            builder.RegisterType<TokenService>().As<ITokenService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();


            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelMappingUser());
                cfg.AddProfile(new ModelToDtoMappingUser());

            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
