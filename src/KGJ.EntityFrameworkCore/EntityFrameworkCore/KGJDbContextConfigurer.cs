using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace KGJ.EntityFrameworkCore
{
    public static class KGJDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<KGJDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<KGJDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
