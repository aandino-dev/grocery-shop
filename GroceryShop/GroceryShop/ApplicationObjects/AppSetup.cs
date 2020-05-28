using Autofac;
using GroceryShop.Models;
using GroceryShop.Services;
using GroceryShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryShop.ApplicationObjects
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder cb)
        {
            cb.RegisterType<ItemsViewModel>().InstancePerDependency();
            cb.RegisterType<MockDataStore>().As<IDataStore<Item>>().SingleInstance();
        }
    }
}
