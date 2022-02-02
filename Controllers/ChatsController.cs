using System.Threading.Tasks;
using DotNetAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers
{
    public class ChatsController : BaseApiController
    {
        private readonly IChatRepository _chatRepository;
        public ChatsController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            return HandleResult(await _chatRepository.GetChats()); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat(string id)
        {
            return HandleResult(await _chatRepository.GetChat(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(string destinationEmail)
        {
            return HandleResult(await _chatRepository.CreateChat(destinationEmail));            
        }
    }
}