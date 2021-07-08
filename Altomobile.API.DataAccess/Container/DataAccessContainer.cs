using Altomobile.API.DataAccess.Connection;
using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altomobile.API.DataAccess.Container
{
    public static class DataAccessContainer
    {
        public static IContainer _container;
        public static IConfiguration _configuration;

        public static void Load(IConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SQLConnection>().As<IConnection<SQLConnection>>();
            builder.RegisterType<Common>().As<ICommon>();

            _container = builder.Build();
            _configuration = configuration;
        }
    }
}
