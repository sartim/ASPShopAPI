using System;
using Microsoft.AspNetCore.Mvc;
using ASPShopAPI.Models;
using ASPShopAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPShopAPI.Models
{
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        [ForeignKey("Permission")]
        public Guid PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}

