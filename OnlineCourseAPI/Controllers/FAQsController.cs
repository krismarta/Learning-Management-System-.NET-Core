
using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQsController : BaseController<FAQ, FAQRepository, int>
    {
        public FAQsController(FAQRepository faqRepository) : base(faqRepository)
        {

        }
    }
}
