using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCourseAPI.Context;
using OnlineCourseClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineCourseClient.Controllers
{
    //[Authorize]
    public class MainController : Controller
    {
        private readonly MyContext context;
        public MainController(MyContext myContext)
        {
            context = myContext;
        }
        public IActionResult Index()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<Claim> claim = identity.Claims;
                ViewData["is_Authentication"] = "1";
                ViewData["Email"] = claim.Where(x => x.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();
                ViewData["role"] = claim.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
                //get all categories
                var allCategories = context.Categories.ToList();
                ViewBag.Allcategories = allCategories.Select(x => x.name);
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
            return View();
            
        }

      
    }
}
