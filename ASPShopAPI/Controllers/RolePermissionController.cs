﻿using System;
using ASPShopAPI.Data;
using ASPShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPShopAPI.Controllers
{
    [Route("api/v1/role-permission")]
    [ApiController]
    public class RolePermissionController : BaseController<RolePermission>
    {
        public RolePermissionController(ShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}

