using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using HRHUB.Helper_Classes;
using HRHUB.Models;

namespace HRHUB.Controllers
{
    public class LoginController : ApiController
    {
        private HREntities db = new HREntities();

        // GET: api/Login
        public IQueryable<UserInfo> GetUserInfoes()
        {
            return db.UserInfoes;
        }

        // GET: api/Login/5
        [HttpGet]
        [Route("api/login")]
        public IHttpActionResult GetUserInfo(string username)
        {
            
            if (DBOperations.IsUsernameExist(username) == false)
            {
                return NotFound();
            }
            return Ok(username);
        }

        // PUT: api/Login/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserInfo(int id, UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userInfo.Employee_ID)
            {
                return BadRequest();
            }

            db.Entry(userInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Login")]
        // POST: api/Login
        [ResponseType(typeof(UserInfo))]
        public IHttpActionResult PostUserInfo(string username,string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string message = "";
            message = DBOperations.LoginAttempt(username,password);
            if (message == "Successfull")
            {
                

                return Ok("Successfull Login");
            }
            else
            {
                /*
                TempData["msg"] = "<script>alert('Username or Password is Incorrect')</script>";
                return View();*/
                return BadRequest("Username or Password is Inncorect");
            }

            /*
            db.UserInfoes.Add(userInfo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserInfoExists(userInfo.Employee_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userInfo.Employee_ID }, userInfo);*/
        }

        // DELETE: api/Login/5
        [ResponseType(typeof(UserInfo))]
        public IHttpActionResult DeleteUserInfo(int id)
        {
            UserInfo userInfo = db.UserInfoes.Find(id);
            if (userInfo == null)
            {
                return NotFound();
            }

            db.UserInfoes.Remove(userInfo);
            db.SaveChanges();

            return Ok(userInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserInfoExists(int id)
        {
            return db.UserInfoes.Count(e => e.Employee_ID == id) > 0;
        }
    }
}