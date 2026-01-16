using Microsoft.EntityFrameworkCore;
using ASPShopAPI.Data;

namespace ASPShopAPI.Tests.TestHelpers
{
    public static class DbContextHelper
    {
        public static ShopDbContext GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new ShopDbContext(options);
        }
    }
}
