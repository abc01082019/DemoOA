
namespace MAP.OA.IDAL
{
    public partial interface IDbSession
    {
			IOrderInfoDal OrderInfoDal { get; }
			IUserInfoDal UserInfoDal { get; }
	        int SaveChanges();
    }
}