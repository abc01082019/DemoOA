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
    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
        public IQueryable<ActionInfo> LoadPageData(Model.Param.ActionQueryParam actionQueryParam)
        {
            var temp = CurrentDal.GetEntities(u => u.DelFlag == (short)DelFlagEnum.Normal);

            if (!string.IsNullOrEmpty(actionQueryParam.SrchName))
            {
                temp = temp.Where(u => u.ActionName.Contains(actionQueryParam.SrchName));
            }

            if (!string.IsNullOrEmpty(actionQueryParam.SrchRemark))
            {
                temp = temp.Where(u => u.Remark.Contains(actionQueryParam.SrchRemark));
            }

            actionQueryParam.Total = temp.Count();

            // Pagination
            return temp.OrderBy(u => u.Id)
                .Skip(actionQueryParam.PageSize * (actionQueryParam.PageIndex - 1))
                .Take(actionQueryParam.PageSize).AsQueryable();

        }

        public bool SetRole(int id, List<int> roleIds)
        {
            var enitity = CurrentDal.GetEntities(u => u.Id == id).FirstOrDefault();

            // remove all roles first
            enitity.RoleInfo.Clear();

            var allSeletedRoles = DbSession.RoleInfoDal.GetEntities(r => roleIds.Contains(r.Id));

            foreach (var roleInfo in allSeletedRoles)
            {
                // add new roles
                enitity.RoleInfo.Add(roleInfo);
            }
            DbSession.SaveChanges();
            return true;
        }
    }
}
