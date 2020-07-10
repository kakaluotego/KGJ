using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using KGJ.Configuration;
using KGJ.Web;

namespace KGJ.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class KGJDbContextFactory : IDesignTimeDbContextFactory<KGJDbContext>
    {
        public KGJDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<KGJDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            KGJDbContextConfigurer.Configure(builder, configuration.GetConnectionString(KGJConsts.ConnectionStringName));

            return new KGJDbContext(builder.Options);
        }
    }
}
