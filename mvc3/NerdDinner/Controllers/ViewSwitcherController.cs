using System.Web.Mvc;

namespace NerdDinner.Controllers
{
    public class ViewSwitcherController : Controller
    {
        public RedirectResult SwitchView(bool mobile, string returnUrl)
        {
            if (Request.Browser.IsMobileDevice == mobile)
            {
                HttpContext.ClearOverriddenBrowser();
            }
            else
            {
                HttpContext.SetOverriddenBrowser(mobile ? BrowserOverride.Mobile : BrowserOverride.Desktop);
            }
            return Redirect(returnUrl);
        }
    }
}
