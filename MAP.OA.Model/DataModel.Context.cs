﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MAP.OA.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataModelContainer : DbContext
    {
        public DataModelContainer()
            : base("name=DataModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<OrderInfo> OrderInfo { get; set; }
        public DbSet<RoleInfo> RoleInfo { get; set; }
        public DbSet<ActionInfo> ActionInfo { get; set; }
        public DbSet<UserInfoExt> UserInfoExt { get; set; }
        public DbSet<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
    }
}
