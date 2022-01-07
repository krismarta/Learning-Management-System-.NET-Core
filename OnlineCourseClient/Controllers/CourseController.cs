
using Microsoft.AspNetCore.Hosting;
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
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineCourseClient.Controllers
{
    public class CourseController : BaseController<Course, CourseRepository, int>
    {
        private readonly MyContext context;
        private readonly CourseRepository courseRepository;
        private readonly SubCourseRepository subcourseRepository;
        private readonly ReviewRepository reviewRepository;
  
        public CourseController(ReviewRepository reviewRepository, CourseRepository repository, MyContext myContext,SubCourseRepository subCourseRepository) : base(repository)
        {
            this.courseRepository = repository;
            this.subcourseRepository = subCourseRepository;
            this.reviewRepository = reviewRepository;
            context = myContext;
        
        }

        public IActionResult Index(int? page)
        {
            

            var pageNumber = page ?? 1;
            var result = courseRepository.getallCourse();
            List<GetCourseAllVM> getallcourseVM = new List<GetCourseAllVM>();
            var rate = 0.0d;

            for (int i = 0; i < result.Result.Count(); i++)
            {
                GetCourseAllVM selectCourse = new GetCourseAllVM();
                selectCourse.CourseId = result.Result[i].id;
                selectCourse.tittle = result.Result[i].title;
                selectCourse.description = result.Result[i].description;
                selectCourse.price = result.Result[i].price; 
                selectCourse.thumbnails = result.Result[i].thumbnail;
                var getcategory = context.Categories.Where(x => x.id == result.Result[i].Categoryid).FirstOrDefault();
                var getfeedback = context.ReviewCourses.Where(j => j.Courseid == result.Result[i].id).Count();
                var getrateIE = context.ReviewCourses.Where(x => x.Courseid == result.Result[i].id).Select(y => y.rate).ToList();

                rate = getrateIE.DefaultIfEmpty(0).Average();
                int rates = Convert.ToInt32(rate);
                selectCourse.Category = getcategory.name;
                selectCourse.total_feedback = rates + " (" + getfeedback + " Feedback)";

                var getduration = context.SubCourses.Where(c => c.Courseid == result.Result[i].id).Select(u => u.duration).ToList();
                var durationcourse = getduration.DefaultIfEmpty(0).Sum();

                selectCourse.total_time = durationcourse + " Minutes";

                getallcourseVM.Add(selectCourse);

            }
            var onepageCourse = getallcourseVM.ToPagedList(pageNumber, 8);

            //get all categories
            var allCategories = context.Categories.ToList();
            ViewBag.Allcategories = allCategories.Select(x => x.name);

            ViewBag.Courses = onepageCourse;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<Claim> claim = identity.Claims;
                ViewData["is_Authentication"] = "1";
                ViewData["Email"] = claim.Where(x => x.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();
                ViewData["role"] = claim.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
            }
            else
            {
                ViewData["is_Authentication"] = 0;
            }
            var userCount = context.Users.Count();
            var courseCount = context.Courses.Count();
            var reviewCount = context.ReviewCourses.Count();
            ViewData["userCount"] = userCount;
            ViewData["courseCount"] = courseCount;
            ViewData["reviewCount"] = reviewCount;
            //return Json(getallcourseVM);
            return View();
        }

        [HttpGet("/Course/Detail/{id}")]
        public async Task<IActionResult> DetailAsync(int id, int? pagerev)
        {
            var pageNumber = pagerev ?? 1;
            //cek id
            var is_id = context.Courses.Where(c => c.id == id).FirstOrDefault();
           
            if (is_id != null)
            {

                var result = await subcourseRepository.getallSubCourse(id);
                double rate = 0.0d;
                ViewData["course_id"] = id;
                ViewData["tittle"] = is_id.title;
                ViewData["description"] = is_id.description;
                ViewData["price"] = is_id.price;
                ViewData["thumbnail"] = is_id.thumbnail;
                ViewData["totalsub_course"] = context.SubCourses.Where(x => x.Courseid == is_id.id).Count();
                var getduration = context.SubCourses.Where(c => c.Courseid == is_id.id).Select(u => u.duration).ToList();
                var durationcourse = getduration.DefaultIfEmpty(0).Sum();
                ViewData["duration"] = durationcourse;
                ViewData["sub_course"] = result;
                var getfeedback = context.ReviewCourses.Where(j => j.Courseid == is_id.id).Count();
                var getrateIE = context.ReviewCourses.Where(x => x.Courseid == is_id.id).Select(y => y.rate).ToList();

                rate = getrateIE.DefaultIfEmpty(0).Average();
                int rates = Convert.ToInt32(rate);
                ViewData["rating"] = rates;
                ViewData["feedback"] = rates + " (" + getfeedback + " Feedback)";
                   //review

                var review = reviewRepository.getreview(id);
                List<ReviewCourseVM> getReviewCourse = new List<ReviewCourseVM>();
                for (int i = 0; i < review.Result.Count(); i++)
                {
                    ReviewCourseVM selectreview = new ReviewCourseVM();
                    var getusername = context.Users.Where(c => c.id == review.Result[i].Userid).Select(x => x.name).FirstOrDefault();
                    selectreview.user_name = getusername;
                    selectreview.date_review = review.Result[i].date_review.Day + " / " + review.Result[i].date_review.Month + " / " + review.Result[i].date_review.Year;
                    selectreview.review = review.Result[i].review;
                    selectreview.Rate = review.Result[i].rate;
                    getReviewCourse.Add(selectreview);
    
                }

                var onepageReview = getReviewCourse.ToPagedList(pageNumber, 3);
                ViewData["review_course"] = onepageReview;

                //cek user mycourse
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (User.Identity.IsAuthenticated)
                {
                    IEnumerable<Claim> claim = identity.Claims;
                    ViewData["is_Authentication"] = "1";
                    var email = claim.Where(x => x.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();
                    ViewData["Email"] = email;
                    ViewData["role"] = claim.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
                    var data_user = context.Users.Where(f => f.email == email).FirstOrDefault();
                    ViewData["name_cus"] = data_user.name;
                    ViewData["phone_cus"] = data_user.phone;
                    ViewData["id_cus"] = data_user.id;
                    //get all categories
                    var allCategories = context.Categories.ToList();
                    ViewBag.Allcategories = allCategories.Select(x => x.name);

                    // getmycourse by id
                    var getuserid = context.Users.Where(t => t.email == email).Select(y => y.id).FirstOrDefault();
                    var mycourse = context.MyCourses.Where(r => r.Userid == getuserid && r.Courseid == id).FirstOrDefault();
                    if (mycourse != null)
                    {
                        ViewData["is_have"] = 1;
                    }
                    else
                    {
                        ViewData["is_have"] = 0;
                    }
                }
                else
                {
                    ViewData["is_Authentication"] = 0;
                }

            }
            else
            {
                return RedirectToAction("Index", "Course");

            }
            return View();
        }

      

        [HttpGet]
        public JsonResult GetAllCourse()
        {
            var result = courseRepository.getallCourse();
            List<GetCourseAllVM> getallcourseVM = new List<GetCourseAllVM>();
            var  rate = 0.0d;
            for (int i = 0; i < result.Result.Count(); i++)
            {
                GetCourseAllVM selectCourse = new GetCourseAllVM();
                selectCourse.CourseId = result.Result[i].id;
                selectCourse.tittle = result.Result[i].title;
                selectCourse.description = result.Result[i].description;
                selectCourse.price = result.Result[i].price;
                selectCourse.thumbnails = result.Result[i].thumbnail;
                var getcategory = context.Categories.Where(x => x.id == result.Result[i].Categoryid).FirstOrDefault();
                var getfeedback = context.ReviewCourses.Where(j => j.Courseid == result.Result[i].id).Count();
                var getrateIE = context.ReviewCourses.Where(x => x.Courseid == result.Result[i].id).Select(y => y.rate).ToList();

                rate = getrateIE.DefaultIfEmpty(0).Average();
                selectCourse.Category = getcategory.name;
                selectCourse.total_feedback = rate + " (" + getfeedback + " Feedback)";

                var getduration = context.SubCourses.Where(c => c.Courseid == result.Result[i].id).Select(u => u.duration).ToList();
                var durationcourse = getduration.DefaultIfEmpty(0).Sum();

                selectCourse.total_time = durationcourse + " Minutes";

                getallcourseVM.Add(selectCourse);

            }


            return Json(getallcourseVM);
        }



    }
}
