using Microsoft.Practices.Unity;
using System.Web.Http;
using SuitsupplyTask.Application.Utils;
using SuitsupplyTask.Core.Interfaces;
using SuitsupplyTask.Core.Services;
using SuitsupplyTask.Infrastructure;
using Unity.WebApi;

namespace SuitsupplyTask.Application
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType(typeof(ICommands<>), typeof(Commands<>));
            container.RegisterType(typeof(IQueries<>), typeof(Queries<>));
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IPhotoService, PhotoService>();
            container.RegisterType<IHttpRequestFileUtils, HttpRequestFileUtils>();
            //container.RegisterTypes(AllClasses.FromLoadedAssemblies(), WithMappings.FromMatchingInterface,WithName.Default);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}