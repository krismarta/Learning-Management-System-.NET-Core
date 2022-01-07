
using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using OnlineCourseClient.Base.Controllers;
using OnlineCourseClient.Repositories.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace OnlineCourseClient.Controllers
{
    //[Authorize]
    public class ReviewController : BaseController<ReviewCourse, ReviewRepository, int>
    {
        private readonly ReviewRepository reviewRepository;
        public ReviewController(ReviewRepository repository) : base(repository)
        {
            this.reviewRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

     


    }
}