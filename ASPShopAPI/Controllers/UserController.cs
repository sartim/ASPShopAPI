using System;
using ASPShopAPI.Data;
using ASPShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPShopAPI.Controllers
{
    [Authorize]
    [Route("api/v1")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserDbContext _context;

		public UserController(UserDbContext context)
		{
			_context = context;
		}
        // Create
        [HttpPost("users")]
        public JsonResult Create(User user)
        {
            // hash the password before saving the user
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);

            _context.SaveChanges();

            return new JsonResult(Ok(user));
        }


        // Edit
        [HttpPut("users/{id}")]
        public JsonResult Edit(User user)
        {
            var userInDb = _context.Users.Find(user.Id);

            if (userInDb == null)
                return new JsonResult(NotFound());

            // update the user's password if it has been changed
            if (user.Password != userInDb.Password)
            {
                userInDb.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            userInDb.FirstName = user.FirstName;
            userInDb.Email = user.Email;

            _context.SaveChanges();

            return new JsonResult(Ok(user));
        }


        // Get
        [HttpGet("users/{id}")]
		public JsonResult Get(Guid id)
		{
			var result = _context.Users.Find(id);

			if (result == null)
				return new JsonResult(NotFound());

			return new JsonResult(Ok(result));
		}

		// Delete
		[HttpDelete("users/{id}")]
		public JsonResult Delete(Guid id)
		{
			var result = _context.Users.Find(id);

			if (result == null)
				return new JsonResult(NotFound());

			_context.Users.Remove(result);
			_context.SaveChanges();

			return new JsonResult(NoContent());
		}

		// Get all
		[HttpGet("users")]
		public JsonResult GetAll()
		{
			var result = _context.Users.ToList();
            return new JsonResult(Ok(result));
        }
	}
}

