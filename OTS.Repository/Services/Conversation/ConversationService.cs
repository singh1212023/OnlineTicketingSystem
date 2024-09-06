using Org.BouncyCastle.Tsp;
using OTS.Common.Helpers;
using OTS.Common.RequestModels.Conversation;
using OTS.Common.RequestModels.User;
using OTS.Repository.Interface.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Repository.Services.Conversation
{
    public class ConversationService:IConversationRepository
    {
        public async Task<GenericBaseResult<SaveConversationRequestModel>> SaveConversationDetails(SaveConversationRequestModel Request)
        {
            var response = new GenericBaseResult<SaveConversationRequestModel>(null);
            return response;
        }
        

    }
}
