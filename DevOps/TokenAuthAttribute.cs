using DevOps.Business.Interfaces;
using DevOps.Common;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DevOps
{
    public class TokenAuthAttribute : AuthorizeAttribute
    {
        private string[] _roles;
        private IAuthorizationManager _authorizationManager;
        private IUserManager _userManager;
        public TokenAuthAttribute(params string[] roles)
        {
            _roles = roles;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var requestScope = actionContext.Request.GetDependencyScope();
                _authorizationManager = requestScope.GetService(typeof(IAuthorizationManager)) as IAuthorizationManager;
                _userManager = requestScope.GetService(typeof(IUserManager)) as IUserManager;
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;
                int id = Convert.ToInt32(authToken.Split(':').First());
                int role = Convert.ToInt32(_userManager.GetUser(id).RoleId);
                string UserRole = ((Enums.Roles)role).ToString();
                UserToken token = _authorizationManager.GetToken(id);
                if(authToken != token.Token || !_roles.Contains(UserRole))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}