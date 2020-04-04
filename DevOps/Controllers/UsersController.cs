﻿using DevOps.Business.Interfaces;
using DevOps.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Data.Entity;
using System.Web.Http.Cors;

namespace DevOps.Controllers
{
    [EnableCorsAttribute("*","*","*")]
    public class UsersController : ApiController
    {
        private IUserManager _userManager;

        public UsersController() { }

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public IHttpActionResult GetAllUsers()
        {
            var users = _userManager.GetAllUsers();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [TokenAuth("Admin","ReleaseManager")]
        public IHttpActionResult GetAuthUsers(string role, int organization)
        {
            var users = _userManager.GetAllUsers();
            if(role != "Admin")
            {
                users = users.Where(x => x.OrganisationId == organization).ToList();
            }
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        public IHttpActionResult GetUser(int userId)
        {
            var user = _userManager.GetUser(userId);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        public IHttpActionResult GetAuthUser(string email, string password)
        {
            var user = _userManager.GetAuthUser(email,password);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IHttpActionResult InsertUser(User user)
        {
            bool status = _userManager.InsertUser(user);
            if (status)
                return Ok(status);
            return NotFound();
        }

        [HttpPost]
        public bool UpdateUser(User user)
        {
            return _userManager.UpdateUser(user);
        }

        [HttpPost]
        public IHttpActionResult DeleteUser(int id)
        {
            bool status = _userManager.DeleteUser(id);
            if(status == false)
            {
                return NotFound();
            }
            return Ok(status);
        }
    }
}
