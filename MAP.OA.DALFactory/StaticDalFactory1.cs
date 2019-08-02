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
	
		public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];

			
		public static IActionInfoDal GetActionInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActionInfoDal") as IActionInfoDal;
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
	
	}
}

