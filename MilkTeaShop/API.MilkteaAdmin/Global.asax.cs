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

        //protected void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        //    if (HttpContext.Current.Request.HttpMethod == "OPTIONS" || HttpContext.Current.Request.HttpMethod == "PUT" || HttpContext.Current.Request.HttpMethod == "DELETE")
        //    {
        //        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, long-key");
        //        HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
        //        HttpContext.Current.Response.End();
        //    }
        //}
    }
}
