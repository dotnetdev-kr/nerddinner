using System;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Web.Mvc;

// in Global.asax.vb Application_Start you can insert these into the ViewEngine chain like so:
//
// ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine())
//
// or
//
// ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine("iPhone")
// {
//     ContextCondition = (ctx => ctx.Request.UserAgent.IndexOf(
//         "iPhone", StringComparison.OrdinalIgnoreCase) >= 0)
// });


public class MobileViewHelper
{
    public static HttpBrowserCapabilitiesBase GetOverriddenBrowser(HttpContextBase httpContext)
    {
        return httpContext.GetOverriddenBrowser();
    }
}

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

    public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
    {
        return NewFindView(controllerContext, viewName, null, useCache, false);
    }

    public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
    {
        return NewFindView(controllerContext, partialViewName, null, useCache, true);
    }

    private ViewEngineResult NewFindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache, bool isPartialView)
    {
        if (!CheckMobileAndCookie(controllerContext.HttpContext))
        {
            // we found nothing and we pretend we looked nowhere
            return new ViewEngineResult(new string[] { });
        }

        // Get the name of the controller from the path
        var controller = controllerContext.RouteData.Values["controller"].ToString();
        var area = "";
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
        var keyPath = Path.Combine(area, controller, newViewName);

        var cacheLocation = ViewLocationCache.GetViewLocation(controllerContext.HttpContext, keyPath);

        // Try the cache          
        if (useCache)
        {
            //If using the cache, check to see if the location is cached.                              
            if (!string.IsNullOrWhiteSpace(cacheLocation))
            {
                return isPartialView ? new ViewEngineResult(this.CreatePartialView(controllerContext, cacheLocation), this) : new ViewEngineResult(this.CreateView(controllerContext, cacheLocation, masterName), this);
            }
        }
        var locationFormats = string.IsNullOrEmpty(area) ? ViewLocationFormats : AreaViewLocationFormats;

        // for each of the paths defined, format the string and see if that path exists. When found, cache it.          
        foreach (var currentPath in locationFormats.Select(rootPath => string.IsNullOrEmpty(area) ? string.Format(rootPath, newViewName, controller) : string.Format(rootPath, newViewName, controller, area)).Where(currentPath => this.FileExists(controllerContext, currentPath)))
        {
            this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, keyPath, currentPath);

            return isPartialView ? new ViewEngineResult(this.CreatePartialView(controllerContext, currentPath), this) : new ViewEngineResult(this.CreateView(controllerContext, currentPath, masterName), this);
        }
        return new ViewEngineResult(new string[] { });
        // we found nothing and we pretend we looked nowhere
    }

    private bool CheckMobileAndCookie(HttpContextBase context)
    {
        if (ContextCondition(context))
        {
            return context.GetOverriddenBrowser().IsMobileDevice;
        }
        return false;
    }
}

public enum BrowserOverride
{
    Desktop = 0,
    Mobile = 1
}

public class CookieBrowserOverrideStore
{
    static internal readonly string BrowserOverrideCookieName = ".ASPXBrowserOverride";
    private readonly int daysToExpire;

    public CookieBrowserOverrideStore()
        : this(1)
    {
    }

    public CookieBrowserOverrideStore(int daysToExpire)
    {
        this.daysToExpire = daysToExpire;
    }

    public string GetOverriddenUserAgent(HttpContextBase httpContext)
    {
        if (httpContext.Response.Cookies.AllKeys.Contains(BrowserOverrideCookieName))
        {
            var cookie = httpContext.Response.Cookies[BrowserOverrideCookieName];
            return cookie != null ? cookie.Value : null;
        }
        var httpCookie = httpContext.Request.Cookies[BrowserOverrideCookieName];
        return httpCookie != null ? httpCookie.Value : null;
    }

    public void SetOverriddenUserAgent(HttpContextBase httpContext, string userAgent)
    {
        var cookie = new HttpCookie(BrowserOverrideCookieName, HttpUtility.UrlEncode(userAgent));
        if ((userAgent == null))
        {
            cookie.Expires = DateTime.Now.AddDays(-1);
        }
        else if ((this.daysToExpire > 0))
        {
            cookie.Expires = DateTime.Now.AddDays(Convert.ToDouble(this.daysToExpire));
        }
        httpContext.Response.Cookies.Remove(BrowserOverrideCookieName);
        httpContext.Response.Cookies.Add(cookie);
    }
}

public class BrowserHelpers
{
    public static readonly object BrowserOverrideKey = new object();
    public const string DesktopUserAgent = "Mozilla/4.0 (compatible; MSIE 6.1; Windows XP)";
    public const string MobileUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows CE; IEMobile 8.12; MSIEMobile 6.0)";
    public static readonly object UserAgentKey = new object();
    public static readonly CookieBrowserOverrideStore BrowserOverrideStore = new CookieBrowserOverrideStore();
}

sealed class UserAgentWorkerRequest : SimpleWorkerRequest
{
    private readonly string userAgent;

    public UserAgentWorkerRequest(string userAgent)
        : base(string.Empty, string.Empty, new StringWriter())
    {
        this.userAgent = userAgent;
    }

    public override string GetKnownRequestHeader(int index)
    {
        return (index != 0x27) ? null : this.userAgent;
    }
}

static class HttpContextExtensions
{
    public static void ClearOverriddenBrowser(this HttpContextBase httpContext)
    {
        httpContext.SetOverriddenBrowser(null);
    }

    private static HttpBrowserCapabilitiesBase CreateOverriddenBrowser(string userAgent)
    {
        return new HttpBrowserCapabilitiesWrapper(new HttpContext(new UserAgentWorkerRequest(userAgent)).Request.Browser);
    }

    public static HttpBrowserCapabilitiesBase GetOverriddenBrowser(this HttpContextBase httpContext)
    {
        return httpContext.GetOverriddenBrowser(CreateOverriddenBrowser);
    }

    static internal HttpBrowserCapabilitiesBase GetOverriddenBrowser(this HttpContextBase httpContext, Func<string, HttpBrowserCapabilitiesBase> createBrowser)
    {
        var browser = (HttpBrowserCapabilitiesBase)httpContext.Items[BrowserHelpers.BrowserOverrideKey];
        if (browser == null)
        {
            var overriddenUserAgent = httpContext.GetOverriddenUserAgent();
            if (string.IsNullOrEmpty(overriddenUserAgent) || string.Equals(overriddenUserAgent, httpContext.Request.UserAgent))
            {
                browser = httpContext.Request.Browser;
            }
            else
            {
                browser = createBrowser.Invoke(overriddenUserAgent);
            }
            httpContext.Items[BrowserHelpers.BrowserOverrideKey] = browser;
        }
        return browser;
    }

    public static string GetOverriddenUserAgent(this HttpContextBase httpContext)
    {
        var result = httpContext.Request.Cookies[CookieBrowserOverrideStore.BrowserOverrideCookieName];
        if (result != null)
        {
            return result.Value;
        }
        return null;
    }

    public static void SetOverriddenBrowser(this HttpContextBase httpContext, string userAgent)
    {
        httpContext.Items[BrowserHelpers.UserAgentKey] = userAgent;
        httpContext.Items[BrowserHelpers.BrowserOverrideKey] = null;
        BrowserHelpers.BrowserOverrideStore.SetOverriddenUserAgent(httpContext, userAgent);
    }

    public static void SetOverriddenBrowser(this HttpContextBase httpContext, BrowserOverride browserOverride)
    {
        string userAgent = null;
        switch (browserOverride)
        {
            case BrowserOverride.Desktop:
                userAgent = "Mozilla/4.0 (compatible; MSIE 6.1; Windows XP)";
                break;
            case BrowserOverride.Mobile:
                userAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows CE; IEMobile 8.12; MSIEMobile 6.0)";
                break;
        }
        httpContext.SetOverriddenBrowser(userAgent);
    }
}
