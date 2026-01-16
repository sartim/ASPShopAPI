using System;
using Microsoft.AspNetCore.Mvc;
using ASPShopAPI.Models;
using ASPShopAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPShopAPI.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : BaseController<User>
    {

        public UserController(ShopDbContext dbContext) : base(dbContext)
        {
        }

        // POST: api/user
        [HttpPost]
        public override async Task<ActionResult<User>> Post(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return await base.Post(user);
        }

    }
}
