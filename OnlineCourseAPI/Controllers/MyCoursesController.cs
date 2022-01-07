
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;
using OnlineCourseAPI.ViewModel;
using System.Net;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyCoursesController : BaseController<MyCourse, MyCourseRepository, int>
    {
        private MyCourseRepository myCourseRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;
        public MyCoursesController(MyContext myContext, MyCourseRepository myCourseRepository, IConfiguration configuration) : base(myCourseRepository)
        {
            this.myCourseRepository = myCourseRepository;
            this._configuration = configuration;
            context = myContext;
        }

        // [Authorize(Roles = "Admin")]
        [HttpGet("UserCourse/{userid}")]
        public ActionResult<GetUserCourseVM> GetUserCourse(string userid)
        {

            var result = myCourseRepository.GetUserCourse(userid);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ada" });
        }



    }
}
