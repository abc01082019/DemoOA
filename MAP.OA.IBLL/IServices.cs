﻿using MAP.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.IBLL
{
	
	public partial interface IActionInfoService: IBaseService<ActionInfo>
	{
	}
	
	public partial interface IFileInfoService: IBaseService<FileInfo>
	{
	}
	
	public partial interface IOrderInfoService: IBaseService<OrderInfo>
	{
	}
	
	public partial interface IR_UserInfo_ActionInfoService: IBaseService<R_UserInfo_ActionInfo>
	{
	}
	
	public partial interface IRoleInfoService: IBaseService<RoleInfo>
	{
	}
	
	public partial interface IUserInfoService: IBaseService<UserInfo>
	{
	}
	
	public partial interface IUserInfoExtService: IBaseService<UserInfoExt>
	{
	}
	
	public partial interface IWF_InstanceService: IBaseService<WF_Instance>
	{
	}
	
	public partial interface IWF_ProcedureService: IBaseService<WF_Procedure>
	{
	}
	
	public partial interface IWF_TempService: IBaseService<WF_Temp>
	{
	}
}

