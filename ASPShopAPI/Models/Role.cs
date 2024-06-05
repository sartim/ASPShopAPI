using System;
namespace ASPShopAPI.Models

{
    public class Role : Base
	{
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}

