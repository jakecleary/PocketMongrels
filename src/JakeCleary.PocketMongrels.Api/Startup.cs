using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using JakeCleary.PocketMongrels.Api;
using JakeCleary.PocketMongrels.Api.Filter;
using JakeCleary.PocketMongrels.Data;
using JakeCleary.PocketMongrels.Data.InMemory;
using JakeCleary.PocketMongrels.Services;
using Microsoft.Owin;
using Microsoft.Web.Http.Versioning;
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

            // Register services.
            builder.RegisterType<UserService>().SingleInstance();
            builder.RegisterType<AnimalService>().SingleInstance();

            // Register repositories.
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();
            builder.RegisterType<AnimalRepository>().As<IAnimalRepository>().SingleInstance();
            
            var lifetimeScope = builder.Build();
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(lifetimeScope);

            // Register any filters.
            Filters(config);

            // Version the api.
            config.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
                options.ReportApiVersions = true;
            });

            app.UseAutofacMiddleware(lifetimeScope);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }

        public void Filters(HttpConfiguration config)
        {
            config.Filters.Add(new ValidateModelAttribute());
        }
    }
}
