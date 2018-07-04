﻿using API.MilkteaAdmin;
using API.MilkteaAdmin.Provider;
using Core.AppService.Database.Identity;
using DependencyResolver.Modules;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Modules;
using Owin;
using System;
using System.Collections.Generic;
using System.Reflection;

[assembly: OwinStartup(typeof(Startup))]

namespace API.MilkteaAdmin
{
    public class Startup
    {
        private static IKernel _kernel;

        public static IKernel Kernel
        {
            get
            {
                if (_kernel == null)
                {
                    _kernel = new StandardKernel();
                    _kernel.Load(Assembly.GetExecutingAssembly());

                    List<NinjectModule> modules = new List<NinjectModule>()
                    {
                        new InfrastructureModule(),
                        new ServiceModule()
                    };
                    _kernel.Load(modules);
                }
                return _kernel;
            }
        }

        public void Configuration(IAppBuilder app)
        {

            //Middleware
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(3),
                AllowInsecureHttp = true,
                Provider = new CustomOAuthorAuthorization(
                    Kernel.Get<IIdentityService>())
            });

            //Middle
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
        }
    }
}