using Altomobile.API.BusinessLogic;
using Altomobile.API.DataAccess.Container;
using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altomobile.API.BusinessLogic.Container
{
    public static class Container
    {
        public static IContainer _container;
        public static IConfiguration _configuration;

        public static void Load(IConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Security>().As<ISecurity>();
            builder.RegisterType<Users>().As<IUsers>();
            builder.RegisterType<Cars>().As<ICars>();

            _container = builder.Build();
            _configuration = configuration;

            DataAccessContainer.Load(configuration);
        }
    }
}
