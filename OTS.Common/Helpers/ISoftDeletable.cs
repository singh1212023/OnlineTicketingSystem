using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.Helpers
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; }
    }
}
