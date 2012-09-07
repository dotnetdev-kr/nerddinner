using System.Web.Mvc;
 
[assembly: WebActivator.PreApplicationStartMethod(typeof(NerdDinner.MobileViewEngines), "Start")]
namespace NerdDinner 
{
    public static class MobileViewEngines
    {
        public static void Start()
        {
            ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine());
        }
    }
}