namespace API.MilkteaAdmin
{
    using API.MilkteaAdmin.Mapper;
    using System;
    using System.Web;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfiguration.Configure();
        }
    }
}
