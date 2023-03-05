using DAL.Extensions;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkWaveAPI.ApiRequestModels;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ChatsController : Controller
    {
        IRepository<Chat> repository;
        UserManager<User> user;

        public ChatsController(IRepository<Chat> repository, UserManager<User> userManager)
        {
            this.repository = repository;
            this.user = userManager;
        }

        [HttpPost]
        [Route("/send")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SendMessage(SendMessageModel model)
        {
            if (ModelState.IsValid)
            {
                var chat = repository.GetById(model.ChatId);

                var message = new Message()
                {
                    SenderId = model.UserId,
                    ChatId = model.ChatId,
                    CreatedOn = DateTime.UtcNow,
                    Value = model.Message,
                    Chat = chat,
                    Sender = await user.FindByIdAsync(model.UserId)
                };

                chat.Messages.Add(message);
                await repository.UpdateAsync(chat);
                return Json(chat);
            }
            
            return BadRequest(model);
        }

        [HttpGet]
        [Route("/getMyChats")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetChatForUser()
        {
            User userC = await user.GetUserByClaimsIdentityNameAsync(User.Identity);
            if (userC != null)
            {
                var userIdStr = userC.Id.ToString();
                var chats = repository.GetAll().Where(c => c.Users.FirstOrDefault(u => u.Id == userIdStr) != null);
                return Json(chats);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("/getchatbyid/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetChatById(int id)
        {
            var chat = await repository.GetByIdAsync(id);

            if (chat == null)
                return BadRequest();

            chat.Messages =(ICollection<Message>) chat.Messages.OrderBy(m => m.CreatedOn);

            return Json(chat);

        }
    }
}
