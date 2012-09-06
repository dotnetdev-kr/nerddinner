using System.Web.Mvc;
using Microsoft.Practices.Unity;
using NerdDinner.Helpers;
using NerdDinner.Models;

namespace NerdDinner
{
    public class DependencyConfig
    {
        public static void RegisterDependencyInjection()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IDinnerRepository, DinnerRepository>();
            container.RegisterType<INerdDinners, NerdDinners>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}