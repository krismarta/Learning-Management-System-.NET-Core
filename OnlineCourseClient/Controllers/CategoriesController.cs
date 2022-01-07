
using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Model;
using OnlineCourseClient.Base.Controllers;
using OnlineCourseClient.Repositories.Data;
using System.Diagnostics;

namespace OnlineCourseClient.Controllers
{
    //[Authorize]
    public class CategoriesController : BaseController<Category, CategoryRepository, string>
    {
        private readonly CategoryRepository categoryRepository;
        public CategoriesController(CategoryRepository repository) : base(repository)
        {
            this.categoryRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}