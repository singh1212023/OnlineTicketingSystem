using OTS.Common.RequestModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.Helpers
{
    public class GenericBaseResult<TModel>:BaseResult

    {
        public TModel Result { get; set; }
        
        public GenericBaseResult(TModel model)
        {
            Result= model;
            
        }

        
    }
}
