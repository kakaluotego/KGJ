using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using KGJ.Authorization.Roles;
using KGJ.Authorization.Users;
using KGJ.BasicManagement;
using KGJ.MultiTenancy;
using KGJ.Common;
using KGJ.WareHouse;
using KGJ.MailSetting;
using KGJ.ProductManagement;

namespace KGJ.EntityFrameworkCore
{
    public class KGJDbContext : AbpZeroDbContext<Tenant, Role, User, KGJDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public virtual DbSet<WareHouseInfo> WareHouseInfos { get; set; }

        public virtual DbSet<WareHouseInfoDts> WareHouseInfoDts { get; set; }


        public virtual DbSet<MailSubscribe> MailSubscribes { get; set; }
        public virtual DbSet<SystemCodeGroup> SystemCodeGroups { get; set; }
        public virtual DbSet<SystemCode> SystemCodes { get; set; }

        public virtual DbSet<WareHouseIOForm> WareHouseIOForms { get; set; }
        public virtual DbSet<WareHouseIOFormDts> WareHouseIOFormDts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductClassification> ProductClassifications { get; set; }
        public virtual DbSet<ProductCustomField> ProductCustomFields { get; set; }
        public virtual DbSet<ProductAddFieldData> ProductAddFieldDatas { get; set; }

        public KGJDbContext(DbContextOptions<KGJDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //}
    }
}
