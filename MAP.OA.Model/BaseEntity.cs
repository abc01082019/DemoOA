using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.Model
{
    public abstract class BaseEntity
    {
        // Require all inherited subclasses to have a Id and a DelFlag attribute
        public abstract int Id { get; set; }
        public abstract short DelFlag { get; set; }
    }
}
