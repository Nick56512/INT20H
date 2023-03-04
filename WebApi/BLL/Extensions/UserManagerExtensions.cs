using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<User> GetUserByClaimsIdentityNameAsync(this UserManager<User> userManager,IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            string? currentUserName = claimsIdentity?.FindFirst(ClaimTypes.Name)?.Value;
            User user=await userManager.FindByNameAsync(currentUserName);
            return user;
        }
    }
}
