using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Microsoft.Web.Mvc
{
    // in Global.asax.cs Application_Start you can insert these into the ViewEngine chain like so:
    //
    // ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine());
    //
    // or
    //
    // ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine("iPhone")
    // {
    //     ContextCondition = (ctx => ctx.Request.UserAgent.IndexOf(
    //         "iPhone", StringComparison.OrdinalIgnoreCase) >= 0)
    // });

    public class MobileCapableRazorViewEngine : RazorViewEngine
    {
        public string ViewModifier { get; set; }
        public Func<HttpContextBase, bool> ContextCondition { get; set; }

        public MobileCapableRazorViewEngine()
            : this("Mobile", context => context.Request.Browser.IsMobileDevice)
        {
        }

        public MobileCapableRazorViewEngine(string viewModifier)
            : this(viewModifier, context => context.Request.Browser.IsMobileDevice)
        {
        }

        public MobileCapableRazorViewEngine(string viewModifier, Func<HttpContextBase, bool> contextCondition)
        {
            this.ViewModifier = viewModifier;
            this.ContextCondition = contextCondition;
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName,
                                                  string masterName, bool useCache)
        {
            return NewFindView(controllerContext, viewName, null, useCache, false);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return NewFindView(controllerContext, partialViewName, null, useCache, true);
        }

        private ViewEngineResult NewFindView(ControllerContext controllerContext, string viewName, string masterName,
                                             bool useCache, bool isPartialView)
        {
            if (!ContextCondition(controllerContext.HttpContext))
            {
                return new ViewEngineResult(new string[] { }); // we found nothing and we pretend we looked nowhere
            }

            // Get the name of the controller from the path
            string controller = controllerContext.RouteData.Values["controller"].ToString();
            string area = "";
            try
            {
                area = controllerContext.RouteData.DataTokens["area"].ToString();
            }
            catch
            {
            }

            // Apply the view modifier
            var newViewName = string.Format("{0}.{1}", viewName, ViewModifier);

            // Create the key for caching purposes          
            string keyPath = Path.Combine(area, controller, newViewName);

            string cacheLocation = ViewLocationCache.GetViewLocation(controllerContext.HttpContext, keyPath);

            // Try the cache          
            if (useCache)
            {
                //If using the cache, check to see if the location is cached.                              
                if (!string.IsNullOrWhiteSpace(cacheLocation))
                {
                    if (isPartialView)
                    {
                        return new ViewEngineResult(CreatePartialView(controllerContext, cacheLocation), this);
                    }
                    else
                    {
                        return new ViewEngineResult(CreateView(controllerContext, cacheLocation, masterName), this);
                    }
                }
            }
            string[] locationFormats = string.IsNullOrEmpty(area) ? ViewLocationFormats : AreaViewLocationFormats;

            // for each of the paths defined, format the string and see if that path exists. When found, cache it.          
            foreach (string rootPath in locationFormats)
            {
                string currentPath = string.IsNullOrEmpty(area)
                                            ? string.Format(rootPath, newViewName, controller)
                                            : string.Format(rootPath, newViewName, controller, area);

                if (FileExists(controllerContext, currentPath))
                {
                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, keyPath, currentPath);

                    if (isPartialView)
                    {
                        return new ViewEngineResult(CreatePartialView(controllerContext, currentPath), this);
                    }
                    else
                    {
                        return new ViewEngineResult(CreateView(controllerContext, currentPath, masterName), this);
                    }
                }
            }
            return new ViewEngineResult(new string[] { }); // we found nothing and we pretend we looked nowhere
        }
    }
}