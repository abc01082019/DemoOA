using MAP.OA.IBLL;
using MAP.OA.Model;

namespace MAP.OA.BLL
{
	
    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
    }
	
    public partial class FileInfoService : BaseService<FileInfo>, IFileInfoService
    {
    }
	
    public partial class OrderInfoService : BaseService<OrderInfo>, IOrderInfoService
    {
    }
	
    public partial class R_UserInfo_ActionInfoService : BaseService<R_UserInfo_ActionInfo>, IR_UserInfo_ActionInfoService
    {
    }
	
    public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
    }
	
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
    }
	
    public partial class UserInfoExtService : BaseService<UserInfoExt>, IUserInfoExtService
    {
    }
	
    public partial class WF_InstanceService : BaseService<WF_Instance>, IWF_InstanceService
    {
    }
	
    public partial class WF_ProcedureService : BaseService<WF_Procedure>, IWF_ProcedureService
    {
    }
	
    public partial class WF_TempService : BaseService<WF_Temp>, IWF_TempService
    {
    }
}

