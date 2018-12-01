using Sample.Core;
using Sample.Interface.IRepo;
using Sample.Interface.IServices;
using Sample.Repository;
using Sample.WebApi.DependencyInjection;
using Sample.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;

namespace Sample.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new CustomExceptionFilter());//Excepiton filter to capture log
            config.Filters.Add(new ValidateModelAttribute());//Validate Model 

            //Dependency Injection
            var container = new UnityContainer();
            //service
            container.RegisterType<IAccountService, AccountService>();

            //Repo
            container.RegisterType<IAccountRepoAsync, AccountRepoAsync>();
            container.RegisterType<ICurrencyRepo, CurrencyRepoAsync>();
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            config.DependencyResolver = new UnityResolver(container);



            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "swagger",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new Swashbuckle.Application.RedirectHandler((message => message.RequestUri.ToString()), "swagger"));


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
