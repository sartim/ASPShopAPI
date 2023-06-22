using System;
using Microsoft.AspNetCore.Mvc;
using ASPShopAPI.Models;
using ASPShopAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPShopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : BaseController<Role>
    {
        public RoleController(ShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
