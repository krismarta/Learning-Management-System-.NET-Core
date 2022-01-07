
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
using System.Linq;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private AccountRepository accountRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;
        public AccountsController(MyContext myContext, AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
            context = myContext;
        }

        [HttpPost("Login")]
        public ActionResult<RegisterVM> GetLogin(RegisterVM registerVM)
        {
            var result = accountRepository.Login(registerVM);
            if (result == 2) //email salah
            {
                return NotFound();
            }
            else if (result == 3) //ChangePassword salah
            {
                return NotFound();
            }
            else if (result == 1)
            {
                
                var getnik = context.Users.Where(r => r.email == registerVM.email).FirstOrDefault();
                var getroleid = getnik.Account.Roleid;

                var getrole = context.Roles.Where(t => t.id == getroleid).FirstOrDefault();

                if (getrole.id == 3)
                {
                    return Ok(new { idtoken = "Unverified", Email = registerVM.email+"|"+getnik.id });
                }
                else
                {
                    var claims = new List<Claim> { };

                    claims.Add(new Claim(ClaimTypes.Email, registerVM.email));
                    claims.Add(new Claim(ClaimTypes.Role, getrole.name));

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(1000),
                        signingCredentials: signIn
                        );

                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                    return Ok(new { idtoken = idtoken, Email = registerVM.email });
                }

            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ada" });
        }


        [HttpPut("Change")]
        public ActionResult<ChangePasswordVM> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var result = accountRepository.ChangePassword(changePasswordVM);
            if (result == 1)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
                
        }

        [HttpPost("Forgot")]
        public ActionResult<RegisterVM> GetForgot(ForgotPasswordVM forgotPasswordVM)
        {
            var result = accountRepository.ForgotPassword(forgotPasswordVM);
            switch (result)
            {
                case 1:
                    return Ok(new { status = HttpStatusCode.OK, result = result, message = "Password berhasil diubah" });
                case 2:
                    return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email tidak ditemukan" });
                case 3:
                    return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Terjadi kesalahan saat pengiriman Email" });
                default:
                    return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Password tidak berhasil diubah" });
               
            }
          
        }

        [HttpGet("verifyAccount/{userid}")]
        public ActionResult verifyAccount(string userid)
        {
            var result = accountRepository.verifyAccount(userid);
            if (result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
