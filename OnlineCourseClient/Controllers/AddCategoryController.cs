using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCourseClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseClient.Controllers
{
    public class AddCategoryController : Controller
    {
        
        public IActionResult Index()
        {
            //var userId = User.GetId();
            //Console.WriteLine(userId);
            return View();
        }
  
    }
}
