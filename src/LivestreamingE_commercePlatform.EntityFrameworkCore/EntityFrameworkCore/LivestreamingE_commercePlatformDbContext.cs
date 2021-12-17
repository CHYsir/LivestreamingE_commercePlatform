using LivestreamingE_commercePlatform.Models;
using LivestreamingE_commercePlatform.Models.Directbroadcastingroom;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace LivestreamingE_commercePlatform.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ConnectionStringName("Default")]
    public class LivestreamingE_commercePlatformDbContext : 
        AbpDbContext<LivestreamingE_commercePlatformDbContext>,
        IIdentityDbContext,
        ITenantManagementDbContext
    {
        /* Add DbSet properties for your Aggregate Roots / Entities here. */

        #region 添加DbSet<>属性
        public DbSet<Room>  Rooms { get; set; }
        public DbSet<Seckill> Seckills { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Goods> Goods { get; set; }
        public virtual DbSet<GoodsClass> GoodsClass { get; set; }
        public virtual DbSet<GoodsImg> GoodsImg { get; set; }
        public virtual DbSet<GoodsInventory> GoodsInventory { get; set; }
        public virtual DbSet<GoodsSpecifications> GoodsSpecifications { get; set; }
        public virtual DbSet<Logistics> Logistics { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<OrderInfo> OrderInfo { get; set; }
        public virtual DbSet<ProCity> ProCity { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleMenus> RoleMenus { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        #endregion

        #region Entities from the modules

        /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityDbContext and ITenantManagementDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */

        //Identity
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }
        
        // Tenant Management
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

        #endregion
        
        public LivestreamingE_commercePlatformDbContext(DbContextOptions<LivestreamingE_commercePlatformDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(LivestreamingE_commercePlatformConsts.DbTablePrefix + "YourEntities", LivestreamingE_commercePlatformConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            #region 添加映射
            //映射到数据库中（采用默认[Table("tb_xxxx")]则不用配置该映射）

            builder.Entity<Room>(b =>
            {
                b.ToTable("tb_room");
                b.ConfigureByConvention();
            });

            builder.Entity<Seckill>(b =>
            {
                b.ToTable("tb_seckills");
                b.ConfigureByConvention();
            });

            builder.Entity<Address>(b =>
            {
                b.ToTable("tb_address");
                b.ConfigureByConvention();
            });

            builder.Entity<Goods>(b =>
            {
                b.ToTable("tb_goods");
                b.ConfigureByConvention();
            });

            builder.Entity<GoodsClass>(b =>
            {
                b.ToTable("tb_goodsclass");
                b.ConfigureByConvention();
            });

            builder.Entity<GoodsImg>(b =>
            {
                b.ToTable("tb_goodsimg");
                b.ConfigureByConvention();
            });

            builder.Entity<GoodsInventory>(b =>
            {
                b.ToTable("tb_goodsinventory");
                b.ConfigureByConvention();
            });

            builder.Entity<GoodsSpecifications>(b =>
            {
                b.ToTable("tb_goodsspecifications");
                b.ConfigureByConvention();
            });

            builder.Entity<Logistics>(b =>
            {
                b.ToTable("tb_logistics");
                b.ConfigureByConvention();
            });

            builder.Entity<Members>(b =>
            {
                b.ToTable("tb_members");
                b.ConfigureByConvention();
            });

            builder.Entity<Menus>(b =>
            {
                b.ToTable("tb_menus");
                b.ConfigureByConvention();
            });

            builder.Entity<OrderDetails>(b =>
            {
                b.ToTable("tb_orderDetails");
                b.ConfigureByConvention();
            });

            builder.Entity<OrderInfo>(b =>
            {
                b.ToTable("tb_orderInfo");
                b.ConfigureByConvention();
            });

            builder.Entity<ProCity>(b =>
            {
                b.ToTable("tb_proCity");
                b.ConfigureByConvention();
            });

            builder.Entity<Role>(b =>
            {
                b.ToTable("tb_role");
                b.ConfigureByConvention();
            });

            builder.Entity<RoleMenus>(b =>
            {
                b.ToTable("tb_rolemenus");
                b.ConfigureByConvention();
            });

            builder.Entity<User>(b =>
            {
                b.ToTable("tb_user");
                b.ConfigureByConvention();
            });

            builder.Entity<UserInfo>(b =>
            {
                b.ToTable("tb_userinfo");
                b.ConfigureByConvention();
            });

            builder.Entity<UserRole>(b =>
            {
                b.ToTable("tb_userrole");
                b.ConfigureByConvention();
            });

            #endregion

            //数据库迁移
            //1、add - migration / update - database
            //2、直接在.EntifyFrwaemworkCore目录下（控制台cmd）输入以下命令：推荐
            // > dotnet ef migrations add create-member - entity        //create-member-entity是迁移取的名称
            //> dotnet ef database update
        }
    }
}
