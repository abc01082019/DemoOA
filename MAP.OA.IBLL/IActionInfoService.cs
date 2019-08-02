using MAP.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.IBLL
{
    public partial interface IActionInfoService : IBaseService<ActionInfo>
    {
        IQueryable<ActionInfo> LoadPageData(Model.Param.ActionQueryParam actionQueryParam);

        bool SetRole(int id, List<int> roleIds);
    }
}
