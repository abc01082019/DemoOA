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
			
		public static IOrderInfoDal _GetOrderInfoDal()
		{
			return new OrderInfoDal();
		}
			
		public static IRoleInfoDal _GetRoleInfoDal()
		{
			return new RoleInfoDal();
		}
			
		public static IUserInfoDal _GetUserInfoDal()
		{
			return new UserInfoDal();
		}
	
		public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];

			
		public static IOrderInfoDal GetOrderInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDal") as IOrderInfoDal;
		} 
			
		public static IRoleInfoDal GetRoleInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".RoleInfoDal") as IRoleInfoDal;
		} 
			
		public static IUserInfoDal GetUserInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDal") as IUserInfoDal;
		} 
	
	}
}

