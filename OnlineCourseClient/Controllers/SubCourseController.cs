using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCourseAPI.Context;
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
using X.PagedList;

namespace OnlineCourseClient.Controllers
{
    public class SubCourseController : BaseController<SubCourse, SubCourseRepository, int>
    {
        private readonly MyContext context;
        private readonly SubCourseRepository subcourseRepository;
        public SubCourseController(SubCourseRepository repository, MyContext myContext) : base(repository)
        {
            this.subcourseRepository = repository;
            context = myContext;
        }

        public IActionResult Index(int? page)
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetsubCourse(int id)
        {
            var result = await subcourseRepository.Get(id);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> getbycourse(int id)
        {
            var result = await subcourseRepository.getbycourse(id);
            return Json(result);
        }


    }
}
