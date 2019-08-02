using MAP.OA.IBLL;
using MAP.OA.Model;
using MAP.OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.BLL
{
    public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
        public IQueryable<RoleInfo> LoadPageData(Model.Param.UserQueryParam userQueryParam)
        {
            var temp = CurrentDal.GetEntities(u => u.DelFlag == (short)DelFlagEnum.Normal);

            if (!string.IsNullOrEmpty(userQueryParam.SrchName))
            {
                temp = temp.Where(u => u.RoleName.Contains(userQueryParam.SrchName));
            }

            if (!string.IsNullOrEmpty(userQueryParam.SrchRemark))
            {
                temp = temp.Where(u => u.Remark.Contains(userQueryParam.SrchRemark));
            }

            userQueryParam.Total = temp.Count();

            // Pagination
            return temp.OrderBy(u => u.Id)
                .Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                .Take(userQueryParam.PageSize).AsQueryable();

        }
    }
}
