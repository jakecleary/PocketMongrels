using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using JakeCleary.PocketMongrels.Core.Entity;
using JakeCleary.PocketMongrels.Data;
using JakeCleary.PocketMongrels.Data.InMemory;
using JakeCleary.PocketMongrels.Server;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace JakeCleary.PocketMongrels.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Repository<User>>().As<IRepository<User>>().SingleInstance();
            builder.RegisterType<Repository<Animal>>().As<IRepository<Animal>>().SingleInstance();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var lifetimeScope = builder.Build();

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(lifetimeScope);
            app.UseAutofacMiddleware(lifetimeScope);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}
