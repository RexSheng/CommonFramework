﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommonFramework.Test.SqlServer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebAPIDemoEntities : DbContext
    {
        public WebAPIDemoEntities()
            : base("name=WebAPIDemoEntities")
        {
        }

        public WebAPIDemoEntities(string conn)
            : base(conn)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<R_UserRole> R_UserRole { get; set; }
        public virtual DbSet<TransferHistory> TransferHistory { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<ViewConfig> ViewConfig { get; set; }
        public virtual DbSet<gn_VW_UserInfo> gn_VW_UserInfo { get; set; }
    }
}
