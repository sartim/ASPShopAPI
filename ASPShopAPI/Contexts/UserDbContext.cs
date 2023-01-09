using System;
using ASPShopAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace ASPShopAPI.Data;


public class UserDbContext: DbContext
{
	public UserDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; }
}

