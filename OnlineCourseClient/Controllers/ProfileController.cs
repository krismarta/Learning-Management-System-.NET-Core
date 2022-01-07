using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCourseAPI.Context;
using OnlineCourseClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineCourseClient.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly MyContext context;
        public ProfileController(MyContext myContext)
        {
            context = myContext;
        }
        public IActionResult Index()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<Claim> claim = identity.Claims;
                ViewData["is_Authentication"] = "1";
                var mailLogin = claim.Where(x => x.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();
                ViewData["Email"] = mailLogin;
                
                ViewData["role"] = claim.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();

                var user_login = context.Users.Where(d => d.email == mailLogin).ToList();
                var id_user = user_login[0].id;

                ViewData["userid"] = id_user;
                ViewData["name"] = user_login[0].name;
                ViewData["phone"] = user_login[0].phone;
                ViewData["gender"] = user_login[0].gender;
                ViewData["birthdate"] = user_login[0].birthDate;
                var findBank = context.BankAccounts.Where(x => x.Userid == id_user).FirstOrDefault();
                
                ViewData["idrek"] = findBank.id;
                ViewData["no_rekening"] = findBank.no;
                ViewData["name_rekening"] = findBank.holder_name;
                ViewData["name_bank"] = findBank.bank_name;



            }
            else
            {
                ViewData["is_Authentication"] = 0;
            }
            return View();
        }

    }
}
