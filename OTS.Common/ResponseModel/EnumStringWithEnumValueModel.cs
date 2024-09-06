using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.ResponseModel
{
    public class EnumStringWithEnumValueModel<T> where T : struct
    {
        public string EnumString { get; set; }
        public T EnumValue { get; set; }
    }
}
