//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    [Serializable]
    public partial class R_UserInfo_ActionInfo
    {
        public int Id { get; set; }
        public Nullable<bool> HasPermission { get; set; }
        public short DelFlag { get; set; }
        public int UserInfoId { get; set; }
        public int ActionInfoId { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
        public virtual ActionInfo ActionInfo { get; set; }
    }
}
