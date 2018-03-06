using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApplication.Models;

namespace MyApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("Login");
        }

        [AllowAnonymous]
        public ActionResult Login(Account account)
        {
            DbUsers dbUsers = new DbUsers(account);
            if (dbUsers.Account.UserName != null && dbUsers.Account.Password != null)
            {
                return RedirectToAction("Index", "Main", account);
                //return View("Main");
            }
            else
            {
                account.UserName = "Invalid Credentials";
                return View("Login");
            }
        }
    }

    public class DbUsers
    {
        public Account Account = new Account();
        List<Account> users = new List<Account>
        {
            new Account { UserName="rak",Password="rak",UserRoles="Admin,Viewer,SuperAdmin"},
            new Account { UserName="admin",Password="admin",UserRoles="Admin,SuperAdmin"},
            new Account { UserName="Guest",Password="Guest",UserRoles="Viewer,SuperAdmin"},
            new Account { UserName="q",Password="q",UserRoles="Viewer"},
        };
        public DbUsers(Account account)
        {
            this.Account = users.Where(x => x.UserName == account.UserName && x.Password == account.Password).First();
        }
    }
}