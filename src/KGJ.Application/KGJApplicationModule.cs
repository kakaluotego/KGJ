using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AutoMapper;
using KGJ.Authorization;
using KGJ.MailSetting;
using KGJ.MailSetting.Dto;

namespace KGJ
{
    [DependsOn(
        typeof(KGJCoreModule),
        typeof(AbpAutoMapperModule))]
    public class KGJApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<KGJAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(KGJApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg =>
                {
                    cfg.AddMaps(thisAssembly);
                    cfg.CreateMap<MailSubscribeDto, MailSubscribe>(MemberList.Destination);
                }
            );

        }
    }
}
