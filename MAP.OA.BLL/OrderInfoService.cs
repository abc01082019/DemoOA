using MAP.OA.IBLL;
using MAP.OA.IDAL;
using MAP.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.BLL
{
    public class OrderInfoService : BaseService<OrderInfo>, IOrderInfoService
    {
        //public OrderInfo Add(OrderInfo orderInfo)
        //{
        //    IUserInfoDal userInfo = StaticDalFactory.GetUserInfoDal();
        //    return orderInfo;
        //}

        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.OrderInfoDal;
        }
    }
}
