using System;
using ASPShopAPI.Data;
using ASPShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPShopAPI.Controllers
{
	[Route("api/v1/[controller]/[action]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserDbContext _context;

		public UserController(UserDbContext context)
		{
			_context = context;
		}

		// Create
		[HttpPost]
		public JsonResult Create(User user)
		{
            _context.Users.Add(user);

            _context.SaveChanges();

			return new JsonResult(Ok(user));
        }

        // Edit
        [HttpPut]
        public JsonResult Edit(User user)
        {
            var userInDb = _context.Users.Find(user.Id);

            if (userInDb == null)
                return new JsonResult(NotFound());

            userInDb = user;

            _context.SaveChanges();

            return new JsonResult(Ok(user));
        }

        // Get
        [HttpGet]
		public JsonResult Get(Guid id)
		{
			var result = _context.Users.Find(id);

			if (result == null)
				return new JsonResult(NotFound());

			return new JsonResult(Ok(result));
		}

		// Delete
		[HttpDelete]
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
		[HttpGet]
		public JsonResult GetAll()
		{
			var result = _context.Users.ToList();
            return new JsonResult(Ok(result));
        }
	}
}

