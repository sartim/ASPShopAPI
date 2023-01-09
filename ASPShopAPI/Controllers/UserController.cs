using System;
using ASPShopAPI.Data;
using ASPShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPShopAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserDbContext _context;

		public UserController(UserDbContext context)
		{
			_context = context;
		}

		// Create/Edit
		[HttpPost]
		public JsonResult CreateEdit(User user)
		{
            if (user.Id.ToString() == null)
            {
				_context.Users.Add(user);
            }
            else
            {
				var userInDb = _context.Users.Find(user.Id);

				if (userInDb == null)
					return new JsonResult(NotFound());

				userInDb = user;
            }

			_context.SaveChanges();

			return new JsonResult(Ok(user));
        }
	}
}

