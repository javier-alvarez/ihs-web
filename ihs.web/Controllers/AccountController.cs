using System;
using System.Web.Mvc;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;


namespace ihs.web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WsFederationConfiguration fc =
            FederatedAuthentication.FederationConfiguration.WsFederationConfiguration;

            string request = System.Web.HttpContext.Current.Request.Url.ToString();
            string wreply = request.Substring(0, request.Length - 7);
            SignOutRequestMessage soMessage =
                            new SignOutRequestMessage(new Uri(fc.Issuer), wreply);
            soMessage.SetParameter("wtrealm", fc.Realm);
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            Response.Redirect(soMessage.WriteQueryString());
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
