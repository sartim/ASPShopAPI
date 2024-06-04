using System;
namespace ASPShopAPI.Models
{
	public class User: Base
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int    Phone { get; set; }
        public string Password { get; set; }
        public bool   IsActive { get; set; }
    }
}

