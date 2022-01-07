using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using OnlineCourseClient.Base.Controllers;
using OnlineCourseClient.Models;
using OnlineCourseClient.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseClient.Controllers
{
    public class RegisterController : BaseController<User, RegisterRepository, string>
    {
        private readonly RegisterRepository registerRepository;
        public RegisterController(RegisterRepository repository) : base(repository)
        {
            this.registerRepository = repository;
        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("index", "main");
            }
        }

        [HttpPost]
        public JsonResult RegisterAccount(RegisterVM entity)
        {
            var result = registerRepository.RegisterPost(entity);
            return Json(result);

        }


    }
}
