using MAP.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.IBLL
{
    public partial interface IRoleInfoService : IBaseService<RoleInfo>
    {
        IQueryable<RoleInfo> LoadPageData(Model.Param.UserQueryParam userQueryParam);
    }
}
