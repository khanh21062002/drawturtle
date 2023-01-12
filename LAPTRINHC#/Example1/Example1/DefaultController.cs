using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public class DefaultController : Controller
    {
        public string Index(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Welcome to ASP.NET Core MVC";
            else
                return $"Hello, {id}! Welcome to ASP.NET Core MVC";
        }
    }
}