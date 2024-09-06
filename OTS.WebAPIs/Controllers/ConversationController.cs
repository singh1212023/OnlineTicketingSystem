using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OTS.Common.RequestModels.Conversation;
using OTS.Common.RequestModels.User;
using OTS.Repository.Interface.Conversation;

namespace OTS.WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationRepository conversationRepository;
        public ConversationController(IConversationRepository _conversation)
        {
            conversationRepository = _conversation;   
        }


        [HttpPost]

        public async Task<IActionResult> AddConversationDetails(SaveConversationRequestModel Request )
        {
            var result= conversationRepository.SaveConversationDetails(Request);
            return Ok(result);  
        }
    }
}
