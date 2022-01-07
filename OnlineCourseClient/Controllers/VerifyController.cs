using Microsoft.AspNetCore.Mvc;
using OnlineCourseAPI.Model;
using OnlineCourseClient.Base.Controllers;
using OnlineCourseClient.Repositories.Data;
using System.Linq;

namespace OnlineCourseClient.Controllers
{
    public class VerifyController : BaseController<BankAccount, BankAccountRepository, int>
    {
        private readonly RegisterRepository registerRepository;
        private readonly BankAccountRepository bankAccountRepository;
        private readonly AccountRepository accountRepository;

        public VerifyController(RegisterRepository repository,BankAccountRepository bankAccountRepository,AccountRepository accountRepository) : base(bankAccountRepository)
        {
            this.registerRepository = repository;
            this.bankAccountRepository = bankAccountRepository;
            this.accountRepository = accountRepository;
        }

        [HttpGet("/verify/{key}")]
        public IActionResult Index(string key)
        {
            var result = registerRepository.CheckEmail(key);
            switch (result)
            {
                case System.Net.HttpStatusCode.OK:
                    var keys = Base64Decode(key);
                    string[] decodekeys = keys.Split("|");
                    ViewData["email"] = decodekeys[0];
                    ViewData["userid"] = decodekeys[1];
                    return View();
                case System.Net.HttpStatusCode.NotFound:
                    return RedirectToAction("index", "main");
                case System.Net.HttpStatusCode.BadRequest:
                    return RedirectToAction("index", "main");
                default:
                    return RedirectToAction("index", "main");
            }
        }

 
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        [HttpGet("/check/{no}")]
        public JsonResult check(string no)
        {
            var result = bankAccountRepository.checknoAccount(no);
            return Json(result);
        }

        [HttpGet("/roleupdate/{userid}")]
        public JsonResult roleupdate(string userid)
        {
            var result = accountRepository.UpdateRL(userid);
            return Json(result);
        }


    }
}
