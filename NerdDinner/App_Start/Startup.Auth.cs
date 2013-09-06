using Owin;
using System.Web.Configuration;

namespace NerdDinner
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseSignInCookies();

            var auth_microsoft_clientId = WebConfigurationManager.AppSettings["auth:microsoft:clientId"];
            var auth_microsoft_clientSecret = WebConfigurationManager.AppSettings["auth:microsoft:clientSecret"];
            if (!string.IsNullOrEmpty(auth_microsoft_clientId) && !string.IsNullOrEmpty(auth_microsoft_clientSecret))
            {
                app.UseMicrosoftAccountAuthentication(
                    clientId: auth_microsoft_clientId,
                    clientSecret: auth_microsoft_clientSecret);
            }

            var auth_twitter_consumerKey = WebConfigurationManager.AppSettings["auth:twitter:consumerKey"];
            var auth_twitter_consumerSecret = WebConfigurationManager.AppSettings["auth:twitter:consumerSecret"];
            if (!string.IsNullOrEmpty(auth_twitter_consumerKey) && !string.IsNullOrEmpty(auth_twitter_consumerSecret))
            {
                app.UseTwitterAuthentication(
                   consumerKey: auth_twitter_consumerKey,
                   consumerSecret: auth_twitter_consumerSecret);
            }

            var auth_facebook_appId = WebConfigurationManager.AppSettings["auth:facebook:appId"];
            var auth_facebook_appSecret = WebConfigurationManager.AppSettings["auth:facebook:appSecret"];
            if (!string.IsNullOrEmpty(auth_facebook_appId) && !string.IsNullOrEmpty(auth_facebook_appSecret))
            {
                app.UseFacebookAuthentication(
                   appId: auth_facebook_appId,
                   appSecret: auth_facebook_appSecret);
            }

            app.UseGoogleAuthentication();
        }
    }
}