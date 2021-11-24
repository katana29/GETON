using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        public string ViewName { get; set; }

        public CustomAuthorizeAttribute() 
        {
            ViewName = "AuthorizeFailed";

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }

        void IsUserAuthorized(AuthorizationContext filterContext) {
            //korisnik jeste autorizovan
            if (filterContext.Result == null)
                return;
            if (filterContext.HttpContext.User.Identity.IsAuthenticated) 
            {
                ViewDataDictionary dic = new ViewDataDictionary();
                dic.Add("Message", "Nije vam dozvoljen pristup ovoj stranici zbog vase uloge");
                var result = new ViewResult() { ViewName = this.ViewName, ViewData = dic };
                filterContext.Result = result;
            }
        }
    }
}