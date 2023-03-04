﻿using Microsoft.AspNetCore.Mvc;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class MembersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
