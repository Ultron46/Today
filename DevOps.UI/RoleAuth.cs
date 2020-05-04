using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevOps.UI
{
    public class RoleAuthAttribute : AuthorizeAttribute
    {
        private string[] _roles;
        public RoleAuthAttribute(params string[] roles)
        {
            _roles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase actionContext)
        {
            bool authorize = false;
            if (_roles.Contains(HttpContext.Current.Session["Role"].ToString()))
            {
                authorize = true;
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}