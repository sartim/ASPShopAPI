using System;
namespace ASPShopAPI.Models
{
	public class Permission
	{
        public Guid   Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool   IsDeleted { get; set; }
        public bool   CreatedAt { get; set; }
        public bool   UpdatedAt { get; set; }
        public bool   DeletedAt { get; set; }
    }
}

