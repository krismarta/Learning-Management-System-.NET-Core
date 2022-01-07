
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using OnlineCourseClient.Base.Controllers;
using OnlineCourseClient.Models;
using OnlineCourseClient.Repositories.Data;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineCourseClient.Controllers
{
    [Authorize]
    public class MyCourseController : BaseController<MyCourse, MyCourseRepository, int>
    {
        private readonly MyContext context;
        private readonly MyCourseRepository myCourseRepository;
        private readonly SubCourseRepository subcourseRepository;
        private readonly IWebHostEnvironment env;
        private readonly ReviewRepository reviewRepository;

        public MyCourseController(ReviewRepository reviewRepository, IWebHostEnvironment env, MyCourseRepository repository, MyContext myContext, SubCourseRepository subCourseRepository) : base(repository)
        {
            this.myCourseRepository = repository;
            this.subcourseRepository = subCourseRepository;
            this.reviewRepository = reviewRepository;
            context = myContext;
            this.env = env;
        }

        
        [HttpPost]
        public IActionResult ActionCertificate(CertificateLinkVM entity)
        {
            entity.link = entity.link.Replace("StrTag", "<").Replace("EndTag", ">").Replace("PAGER","#");
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageSize = PdfPageSize.A4;
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            htmlToPdf.Options.MarginLeft = 10;
            htmlToPdf.Options.MarginRight = 10;
            htmlToPdf.Options.MarginTop = 20;
            htmlToPdf.Options.MarginBottom = 20;

            htmlToPdf.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            htmlToPdf.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;

            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(entity.link);
            byte[] pdf = pdfDocument.Save();
            pdfDocument.Close();

            return File(
                    pdf,
                    "application/pdf",
                    "Certificate.pdf"
                );
        }

        public IActionResult Certificate()
        {
            return View();
        }


        public IActionResult Index(int? page)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<Claim> claim = identity.Claims;
            ViewData["is_Authentication"] = "1";
            var mailLogin = claim.Where(x => x.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();
            ViewData["Email"] = mailLogin;

            ViewData["role"] = claim.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();

            var user_login = context.Users.Where(d => d.email == mailLogin).ToList();
            var id_user = user_login[0].id;

            ViewData["userid"] = id_user;
            ViewData["name"] = user_login[0].name;

                var pageNumber = page ?? 1;


                var result = myCourseRepository.GetUserCourse(id_user);
                var X = result;


                List<GetUserCourseVM> getusercourseVM = new List<GetUserCourseVM>();
                //var rate = 0.0d;

                for (int i = 0; i < result.Result.Count(); i++)
                {
                    GetUserCourseVM selectCourse = new GetUserCourseVM();
                    selectCourse.CourseId = result.Result[i].CourseId;
                    selectCourse.CourseTitle = result.Result[i].CourseTitle;
                    selectCourse.CourseDescription = result.Result[i].CourseDescription;
                    selectCourse.CoursePrice = result.Result[i].CoursePrice;
                    selectCourse.CourseThumbnail = result.Result[i].CourseThumbnail;
                  //  selectCourse.CourseThumbnail = env.WebRootFileProvider.GetFileInfo("images/foo.png")?.PhysicalPath;
                    var getcategory = context.Categories.Where(x => x.id == result.Result[i].CategoryId).FirstOrDefault();
                    //  var getfeedback = context.ReviewCourses.Where(j => j.Courseid == result.Result[i].CategoryId).Count();
                    // var getrateIE = context.ReviewCourses.Where(x => x.Courseid == result.Result[i].id).Select(y => y.rate).ToList();

                    // rate = getrateIE.DefaultIfEmpty(0).Average();
                    selectCourse.CategoryName = getcategory.name;
                    //selectCourse.total_feedback = rate + " (" + getfeedback + " Feedback)";

                    var getduration = context.SubCourses.Where(c => c.Courseid == result.Result[i].CourseId).Select(u => u.duration).ToList();
                    var durationcourse = getduration.DefaultIfEmpty(0).Sum();

                    selectCourse.total_time = durationcourse + " Minutes";

                    getusercourseVM.Add(selectCourse);

                }
                var onepageCourse = getusercourseVM.ToPagedList(pageNumber, 8);

                //get all categories
                var allCategories = context.Categories.ToList();
                ViewBag.Allcategories = allCategories.Select(x => x.name);

                ViewBag.Courses = onepageCourse;
                //return Json(getallcourseVM);
               

            }
            else
            {
                ViewData["is_Authentication"] = 0;
            }

            return View();
        }

        [HttpGet("/MyCourse/Detail/{id}")]
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
                    //ViewData["thumbnail"] = env.WebRootFileProvider.GetFileInfo("images/foo.png")?.PhysicalPath;
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

                var identity = HttpContext.User.Identity as ClaimsIdentity;

                if (User.Identity.IsAuthenticated)
                {
                    IEnumerable<Claim> claim = identity.Claims;
                    ViewData["is_Authentication"] = "1";
                    var mailLogin = claim.Where(x => x.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();
                    ViewData["Email"] = mailLogin;

                    ViewData["role"] = claim.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();

                    var user_login = context.Users.Where(d => d.email == mailLogin).FirstOrDefault();
                    var id_user = user_login.id;
                    ViewData["user_name"] = user_login.name;

                    ViewData["userid"] = id_user;
                    var id_reviewcourse = context.ReviewCourses.Where(d => d.Userid == id_user).Where(d => d.Courseid == id).Select(c => c.id).FirstOrDefault();
                    ViewData["reviewcourseid"] = id_reviewcourse;
                    var review = reviewRepository.getreviewuser(id, id_user);


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

            //[HttpGet]
            //public JsonResult GetAllCourse()
            //{
            //    var result = myCourseRepository.getallCourse();
            //    List<GetCourseAllVM> getallcourseVM = new List<GetCourseAllVM>();
            //    var rate = 0.0d;
            //    for (int i = 0; i < result.Result.Count(); i++)
            //    {
            //        GetCourseAllVM selectCourse = new GetCourseAllVM();
            //        selectCourse.CourseId = result.Result[i].id;
            //        selectCourse.tittle = result.Result[i].title;
            //        selectCourse.description = result.Result[i].description;
            //        selectCourse.price = result.Result[i].price;
            //        selectCourse.thumbnails = result.Result[i].thumbnail;
            //        var getcategory = context.Categories.Where(x => x.id == result.Result[i].Categoryid).FirstOrDefault();
            //        var getfeedback = context.ReviewCourses.Where(j => j.Courseid == result.Result[i].id).Count();
            //        var getrateIE = context.ReviewCourses.Where(x => x.Courseid == result.Result[i].id).Select(y => y.rate).ToList();

            //        rate = getrateIE.DefaultIfEmpty(0).Average();
            //        selectCourse.Category = getcategory.name;
            //        selectCourse.total_feedback = rate + " (" + getfeedback + " Feedback)";

            //        var getduration = context.SubCourses.Where(c => c.Courseid == result.Result[i].id).Select(u => u.duration).ToList();
            //        var durationcourse = getduration.DefaultIfEmpty(0).Sum();

            //        selectCourse.total_time = durationcourse + " Minutes";

            //        getallcourseVM.Add(selectCourse);

            //    }



            //[HttpGet]
            //public JsonResult GetAllCourse()
            //{
            //    var result = myCourseRepository.getallCourse();
            //    List<GetCourseAllVM> getallcourseVM = new List<GetCourseAllVM>();
            //    var rate = 0.0d;
            //    for (int i = 0; i < result.Result.Count(); i++)
            //    {
            //        GetCourseAllVM selectCourse = new GetCourseAllVM();
            //        selectCourse.CourseId = result.Result[i].id;
            //        selectCourse.tittle = result.Result[i].title;
            //        selectCourse.description = result.Result[i].description;
            //        selectCourse.price = result.Result[i].price;
            //        selectCourse.thumbnails = result.Result[i].thumbnail;
            //        var getcategory = context.Categories.Where(x => x.id == result.Result[i].Categoryid).FirstOrDefault();
            //        var getfeedback = context.ReviewCourses.Where(j => j.Courseid == result.Result[i].id).Count();
            //        var getrateIE = context.ReviewCourses.Where(x => x.Courseid == result.Result[i].id).Select(y => y.rate).ToList();

            //        rate = getrateIE.DefaultIfEmpty(0).Average();
            //        selectCourse.Category = getcategory.name;
            //        selectCourse.total_feedback = rate + " (" + getfeedback + " Feedback)";

            //        var getduration = context.SubCourses.Where(c => c.Courseid == result.Result[i].id).Select(u => u.duration).ToList();
            //        var durationcourse = getduration.DefaultIfEmpty(0).Sum();

            //        selectCourse.total_time = durationcourse + " Minutes";

            //        getallcourseVM.Add(selectCourse);

            //    }


            //    return Json(getallcourseVM);
            //}

        }
    }


