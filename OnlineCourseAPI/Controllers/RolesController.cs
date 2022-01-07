
using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        public RolesController(RoleRepository roleRepository) : base(roleRepository)
        {

        }
    }
}
