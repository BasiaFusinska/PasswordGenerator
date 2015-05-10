﻿using System;
using System.Web;
using System.Web.Http;
using PasswordGenerator;

namespace PasswordGeneratorWeb
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            HttpContext.Current.Application["Authentication"] = new Authentication(new AccessTokenGenerator(), new AccessTokenRepository(TimeSpan.FromSeconds(30)));
        }
    }
}
