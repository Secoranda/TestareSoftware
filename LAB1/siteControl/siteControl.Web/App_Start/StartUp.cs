using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartupAttribute(typeof(siteControl.Web.App_Start.StartUp))]

namespace siteControl.Web.App_Start
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType= "ApplicationCookie",
                LoginPath = new PathString("/Auth/Login")
            });
        }
    }
}