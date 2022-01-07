
using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNumbersController : BaseController<UserNumber, UserNumberRepository, int>
    {
        public UserNumbersController(UserNumberRepository userNumberRepository) : base(userNumberRepository)
        {

        }
    }
}
