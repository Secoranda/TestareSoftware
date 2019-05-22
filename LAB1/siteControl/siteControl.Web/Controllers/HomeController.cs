using System.Web.Mvc;
using siteControl.Web.Models;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin;


namespace siteControl.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        
        public ActionResult Index()
        {
          
            return View();
        }

      
     
    }
}