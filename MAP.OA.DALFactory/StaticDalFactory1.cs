using MAP.OA.EFDAL;
using MAP.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.DALFactory
{
	public class StaticDalFactory
	{
			
		public static IActionInfoDal _GetActionInfoDal()
		{
			return new ActionInfoDal();
		}
			
		public static IFileInfoDal _GetFileInfoDal()
		{
			return new FileInfoDal();
		}
			
		public static IOrderInfoDal _GetOrderInfoDal()
		{
			return new OrderInfoDal();
		}
			
		public static IR_UserInfo_ActionInfoDal _GetR_UserInfo_ActionInfoDal()
		{
			return new R_UserInfo_ActionInfoDal();
		}
			
		public static IRoleInfoDal _GetRoleInfoDal()
		{
			return new RoleInfoDal();
		}
			
		public static IUserInfoDal _GetUserInfoDal()
		{
			return new UserInfoDal();
		}
			
		public static IUserInfoExtDal _GetUserInfoExtDal()
		{
			return new UserInfoExtDal();
		}
			
		public static IWF_InstanceDal _GetWF_InstanceDal()
		{
			return new WF_InstanceDal();
		}
			
		public static IWF_ProcedureDal _GetWF_ProcedureDal()
		{
			return new WF_ProcedureDal();
		}
			
		public static IWF_TempDal _GetWF_TempDal()
		{
			return new WF_TempDal();
		}
	
		public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];

			
		public static IActionInfoDal GetActionInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActionInfoDal") as IActionInfoDal;
		} 
			
		public static IFileInfoDal GetFileInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".FileInfoDal") as IFileInfoDal;
		} 
			
		public static IOrderInfoDal GetOrderInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDal") as IOrderInfoDal;
		} 
			
		public static IR_UserInfo_ActionInfoDal GetR_UserInfo_ActionInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".R_UserInfo_ActionInfoDal") as IR_UserInfo_ActionInfoDal;
		} 
			
		public static IRoleInfoDal GetRoleInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".RoleInfoDal") as IRoleInfoDal;
		} 
			
		public static IUserInfoDal GetUserInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDal") as IUserInfoDal;
		} 
			
		public static IUserInfoExtDal GetUserInfoExtDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoExtDal") as IUserInfoExtDal;
		} 
			
		public static IWF_InstanceDal GetWF_InstanceDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WF_InstanceDal") as IWF_InstanceDal;
		} 
			
		public static IWF_ProcedureDal GetWF_ProcedureDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WF_ProcedureDal") as IWF_ProcedureDal;
		} 
			
		public static IWF_TempDal GetWF_TempDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WF_TempDal") as IWF_TempDal;
		} 
	
	}
}

