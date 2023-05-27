using System;
using Microsoft.AspNetCore.Mvc;
using ASPShopAPI.Models;
using ASPShopAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPShopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PermissionController : BaseController<User>
    {
        public PermissionController(ShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
