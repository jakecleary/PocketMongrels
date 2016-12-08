﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using JakeCleary.PocketMongrels.Api.Controllers;
using JakeCleary.PocketMongrels.Core;
using JakeCleary.PocketMongrels.Core.Entity;
using JakeCleary.PocketMongrels.Data.Repository;

namespace JakeCleary.PocketMongrels.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ///////////////////////
            // Configure Autofac //
            ///////////////////////
            
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            // Register the controllers.
            builder.RegisterType<UsersController>().InstancePerRequest();
            builder.RegisterType<AnimalsController>().InstancePerRequest();
            builder.RegisterType<ActionsController>().InstancePerRequest();

            // Register the repositories.
            builder.RegisterType<Repository<User>>().As<IRepository<User>>().SingleInstance();
            builder.RegisterType<Repository<Animal>>().As<IRepository<Animal>>().SingleInstance();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
