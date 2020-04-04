using DevOps.Business.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DevOps.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class AuthorizationController : ApiController
    {
        private IAuthorizationManager _authorizationManager;

        public AuthorizationController(IAuthorizationManager authorizationManager)
        {
            _authorizationManager = authorizationManager;
        }
        public IHttpActionResult GetUserToken(int id)
        {
            UserToken userToken = _authorizationManager.GetToken(id);
            if(userToken == null)
            {
                return NotFound();
            }
            return Ok(userToken);
        }
        [HttpPost]
        public IHttpActionResult InsertToken(UserToken userToken)
        {
            bool status = _authorizationManager.InsertUserToken(userToken);
            if(status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }
        [TokenAuth("Admin","ReleaseManager","User"), HttpGet]
        public IHttpActionResult DeleteToken(int id)
        {
            bool status = _authorizationManager.DeleteUserToken(id);
            if(status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }
    }
}
