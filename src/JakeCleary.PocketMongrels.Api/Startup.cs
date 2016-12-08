using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using JakeCleary.PocketMongrels.Api;
using JakeCleary.PocketMongrels.Data;
using JakeCleary.PocketMongrels.Data.InMemory;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace JakeCleary.PocketMongrels.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // Register controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register repositories.
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();
            builder.RegisterType<AnimalRepository>().As<IAnimalRepository>().SingleInstance();
            
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
