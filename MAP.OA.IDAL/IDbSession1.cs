﻿
namespace MAP.OA.IDAL
{
    public partial interface IDbSession
    {
			IActionInfoDal ActionInfoDal { get; }
			IOrderInfoDal OrderInfoDal { get; }
			IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal { get; }
			IRoleInfoDal RoleInfoDal { get; }
			IUserInfoDal UserInfoDal { get; }
			IUserInfoExtDal UserInfoExtDal { get; }
	        int SaveChanges();
    }
}