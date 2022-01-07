
using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCourseLogsController : BaseController<SubCourseLog, SubCourseLogRepository, int>
    {
        public SubCourseLogsController(SubCourseLogRepository subCourseLogRepository) : base(subCourseLogRepository)
        {

        }
    }
}
