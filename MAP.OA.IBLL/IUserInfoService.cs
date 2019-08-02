using MAP.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.IBLL
{
    public partial interface IUserInfoService : IBaseService<UserInfo>
    {
        IQueryable<UserInfo> LoadPageData(Model.Param.UserQueryParam userQueryParam);

        bool SetRole(int id, List<int> roleIds);
    }
}
