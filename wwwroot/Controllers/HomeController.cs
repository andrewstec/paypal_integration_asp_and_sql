using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PayPal.BusinessLayer;
using PayPal.Models;
using PayPal.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PayPal.Controllers
{
    public class HomeController : Controller
    {
        bool CaptchaCheck()
        {
            string captchaResponse;

            if (ModelState.IsValid)
            {
                CaptchaHelper captchaHelper = new CaptchaHelper();
                captchaResponse = captchaHelper.CheckRecaptcha();
            }
            else
            {
                captchaResponse = "fail";
            }

            if (captchaResponse != null && captchaResponse.Equals("Valid"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // GET: Home
        public ActionResult Index()
        {
            Session["StartSession"] = DateTime.UtcNow;
            ViewBag.SessionID = Session.SessionID;
            return View();
        }

        public ActionResult ThankYou()
        {

            return View();
        }

        public ActionResult OrderCancelled()
        {

            return View();
        }

        public ActionResult Welcome()
        {
            Session["StartSession"] = DateTime.UtcNow;
            ViewBag.SuccessfullyRegistered = TempData["SuccessfullyRegistered"];
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            IdentityUser identityUser = manager.Find(login.UserName, login.Password);

            if (ModelState.IsValid)
            {
                if (identityUser != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, login.UserName),
                    },
                    DefaultAuthenticationTypes.ApplicationCookie,
                    ClaimTypes.Name, ClaimTypes.Role);
                    //SignIn is where ClaimsIdentity is accepted to issue a logged in cookie.
                    authenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    },
                    identity);
                    return RedirectToAction("ViewTransactions", "Home");
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register registrant)
        {
            if (CaptchaCheck() == true)
            {

                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);
                var identityUser = new IdentityUser()
                {
                    UserName = registrant.UserName,
                    Email = registrant.Email
                };

                IdentityResult result = manager.Create(identityUser, registrant.Password);

                if (result.Succeeded)
                {
                    var AuthenticationManager = HttpContext.Request.GetOwinContext().Authentication;
                    var userIdentity = manager.CreateIdentity(identityUser, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                }

                TempData["SuccessfullyRegistered"] = "You have successfully registered for your account.";
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewBag.Error = "Please check the Captcha checkbox before registering for an account.";
                return View();
            }

        }

        [Authorize]
        public ActionResult ViewTransactions()
        {
            TransactionRecordRepo transRepo = new TransactionRecordRepo();
            return View(transRepo.GetAllTransactionRecords());
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}