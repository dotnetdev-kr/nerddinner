using System.Web.Mvc;
 
[assembly: WebActivator.PreApplicationStartMethod(typeof(NerdDinner.App_Start.MobileViewEngines), "Start")]
namespace NerdDinner.App_Start {
    public static class MobileViewEngines{
        public static void Start()
        {
            ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine());
        }
    }
}