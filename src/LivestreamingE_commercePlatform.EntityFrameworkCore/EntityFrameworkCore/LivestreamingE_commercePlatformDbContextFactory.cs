using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LivestreamingE_commercePlatform.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class LivestreamingE_commercePlatformDbContextFactory : IDesignTimeDbContextFactory<LivestreamingE_commercePlatformDbContext>
    {
        public LivestreamingE_commercePlatformDbContext CreateDbContext(string[] args)
        {
            LivestreamingE_commercePlatformEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<LivestreamingE_commercePlatformDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new LivestreamingE_commercePlatformDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LivestreamingE_commercePlatform.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
