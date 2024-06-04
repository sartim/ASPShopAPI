using System;
namespace ASPShopAPI.Models
{
	public class User
	{
        public Guid     Id { get; set; }
        public string   FirstName { get; set; }
        public string   Email { get; set; }
        public int      Phone { get; set; }
        public string   Password { get; set; }
        public bool     IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool     DeletedAt { get; set; }
    }
}

