using DAL.Extensions;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class AchivmentController : Controller
    {
        UserManager<User> user;
        AchievmentRepository repository;

        public AchivmentController(UserManager<User> user, AchievmentRepository repository)
        {
            this.user = user;
            this.repository = repository;
        }

        [HttpGet]
        [Route("/getachiementbyid/{id}")]
        public async Task<IActionResult> GetAchievmentById(int id)
        {
            var result = await repository.GetByIdAsync(id);

            return result!= null ? Json(result) : NotFound();
        }

        [HttpGet]
        [Route("/getuserachievment")]
        public async Task<IActionResult> GetUserAchievments() {
            var currentUser = await user.GetUserByClaimsIdentityNameAsync(User.Identity!);

            return currentUser!= null ? Json(currentUser.Achievments) : BadRequest();
        }

        [HttpGet]
        [Route("/checkfornewachievments")]
        public async Task<IActionResult> CheckUserNewAchievments()
        {
            var currentUser = await user.GetUserByClaimsIdentityNameAsync(User.Identity!);
            var achievments = currentUser.Achievments.ToList();
            var newAchievments = new List<Achievment>();
            Achievment achievmentToTest;

            var chatNumber = currentUser.Chats.Count;

            if (chatNumber == 1 || chatNumber % 10 == 0 && chatNumber != 0)
            {
                achievmentToTest = achievments.FirstOrDefault(a => a.Name == $"{chatNumber}th chat")!;
                if (achievmentToTest == null)
                    newAchievments.Add(await repository.GetByName($"Congratulations! It is your {chatNumber}th chat"));
            }
            else if (currentUser.DescriptionUser!=null) {
                achievmentToTest = achievments.FirstOrDefault(a => a.Name == $"I know you)")!;
                if (achievmentToTest == null)
                    newAchievments.Add(await repository.GetByName($"I know you)"));
            }else if (!currentUser.PortfolioProjects.IsNullOrEmpty())
            {
                achievmentToTest = achievments.FirstOrDefault(a => a.Name == $"Your first project very cool!")!;
                if (achievmentToTest == null)
                    newAchievments.Add(await repository.GetByName($"Your first project very cool!"));
            }

            return !newAchievments.IsNullOrEmpty() ? Json(newAchievments) : NotFound();
        }
    }
}
