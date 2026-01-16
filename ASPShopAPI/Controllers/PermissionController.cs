using System;
using Microsoft.AspNetCore.Mvc;
using ASPShopAPI.Models;
using ASPShopAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPShopAPI.Controllers
{
    [Route("api/v1/permissions")]
    [ApiController]
    public class PermissionController : BaseController<Permission>
    {
        public PermissionController(ShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
