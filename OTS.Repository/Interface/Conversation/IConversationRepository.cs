using OTS.Common.Helpers;
using OTS.Common.RequestModels.Conversation;
using OTS.Common.RequestModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Repository.Interface.Conversation
{
    public interface IConversationRepository
    {
        Task<GenericBaseResult<SaveConversationRequestModel>> SaveConversationDetails(SaveConversationRequestModel Request);
    }
}
