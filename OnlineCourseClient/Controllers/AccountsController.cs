
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using OnlineCourseClient.Base.Controllers;
using OnlineCourseClient.Repositories.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnlineCourseClient.Controllers
{
    //[Authorize]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
        }



        //[HttpGet]
        //public async Task<JsonResult> GetRegister()
        //{
        //    var result = await accountRepository.GetRegister();
        //    return Json(result);
        //}


        [HttpPost]
        public JsonResult Forgot(ForgotPasswordVM forgotPasswordVM)
        {
            var result = accountRepository.Forgot(forgotPasswordVM);
            return Json(result);
        }

        [HttpPut]
        public JsonResult Change(ChangePasswordVM changepasswordVM)
        {
            var result = accountRepository.ChangePassword(changepasswordVM);
            return Json(result);
        }

        [HttpPost("Auth/")]
        public JsonResult Auth(RegisterVM login)
        {
            var result = accountRepository.Auth(login);
            if (result.Result.idtoken == "Unverified")
            {
                result.Result.statusCode = "2";

            }
            else if (result.Result.idtoken != null)
            {
                //return RedirectToAction("index");
                result.Result.statusCode = "1";
                HttpContext.Session.SetString("JWToken", result.Result.idtoken);
                HttpContext.Session.SetString("Email", result.Result.email);
            }
            else
            {
                result.Result.statusCode = "0";
            }
            

            return Json(result);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}