
namespace MAP.OA.IDAL
{
    public partial interface IDbSession
    {
			IOrderInfoDal OrderInfoDal { get; }
			IRoleInfoDal RoleInfoDal { get; }
			IUserInfoDal UserInfoDal { get; }
	        int SaveChanges();
    }
}