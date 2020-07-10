using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using KGJ.EntityFrameworkCore.Seed;
using Microsoft.Extensions.Logging;

namespace KGJ.EntityFrameworkCore
{
    [DependsOn(
        typeof(KGJCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class KGJEntityFrameworkModule : AbpModule
    {
        private readonly ILoggerFactory _loggerFactory;

        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public KGJEntityFrameworkModule(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<KGJDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        KGJDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        KGJDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }

                    options.DbContextOptions.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KGJEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
