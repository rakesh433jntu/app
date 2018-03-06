using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using MyApplication.Models;

namespace MyApplication.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        [HandleError]
        public ActionResult Index(Account account)
        {
            return View(account);
        }

        [CustomAuthorizeAttribute(Roles = "admin")]
        public ActionResult myAction()
        {            
            string str = System.Web.HttpContext.Current.User.Identity.Name;
            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(GetUserId());
            return View();
        }

    }

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //filterContext.Result  = new
            Account ac = new Account();
            ac.UserRoles = "Iam a disco dancer";
            CustomPrincipal cp = new CustomPrincipal(ac);

            cp.IsInRole(Roles);


            //GenericIdentity MyPrincipal = new GenericIdentity("Rakesh1");
            IPrincipal Id = (IPrincipal)cp;
            HttpContext.Current.User = Id;
        }
    }

    public class CustomPrincipal : IPrincipal
    {
        private Account Account { get; set; }
        //IIdentity IPrincipal.Identity => new GenericIdentity(Account.UserName);

        public IIdentity Identity { get; }

        public CustomPrincipal(Account account)
        {
            this.Account = account;
            this.Identity = new GenericIdentity("Rakesh");            

        }


        public bool IsInRole(string role)
        {
            string str = this.Account.UserRoles;
            return true;
        }
    }
}