using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersAndPets.App_Start
{
    public static class ObjectLocator
    {
        private readonly static IContainer _container = new Container();

        public static void Configure(Action<ConfigurationExpression> expression)
        {
            _container.Configure(expression);
        }

        public static T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}