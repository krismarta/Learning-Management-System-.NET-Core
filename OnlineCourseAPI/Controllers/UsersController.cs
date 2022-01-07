
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;
using OnlineCourseAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User, UserRepository, string>
    {
        private UserRepository userRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public UsersController(MyContext myContext,UserRepository userRepository, IConfiguration configuration) : base(userRepository)
        {
            this.userRepository = userRepository;
            this._configuration = configuration;
            context = myContext;
        }


        [HttpPost("Register")]
        public ActionResult Post(RegisterVM registerVM)
        {
            var result = userRepository.Register(registerVM);

            switch (result)
            {
                case 1:
                    return Ok(result);
                case 2:
                    return Conflict(new { status = HttpStatusCode.BadRequest, result = result, message = "Email tidak boleh sama" });
                case 3:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Email Verifikasi tidak terkirim" });
                default:
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Data tidak berhasil dibuat" });
            }
        }

        [HttpGet("check/{email}")]
        public ActionResult check(string email)
        {
            var key = Base64Decode(email);
            string[] decodekey = key.Split("|");
            var result = userRepository.checkEmail(decodekey[0]);
            switch (result)
            {
                case 0:
                    return NotFound();
                case 1:
                    return Ok();
                default:
                    return BadRequest();
            }
        }

        // [Authorize(Roles = "Admin")]
        [HttpGet("Register")]
        public ActionResult<RegisterVM> GetRegister()
        {

            var result = userRepository.GetRegister();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ada" });
        }



    }
}
