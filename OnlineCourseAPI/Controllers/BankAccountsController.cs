
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : BaseController<BankAccount, BankAccountRepository, int>
    {
        private BankAccountRepository bankAccountRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public BankAccountsController(MyContext myContext, BankAccountRepository bankAccountRepository, IConfiguration configuration) : base(bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
            this._configuration = configuration;
            context = myContext;
        }

        [HttpGet("check/{no}")]
        public ActionResult check(string no)
        {
            var result = bankAccountRepository.checknoAccount(no);
            switch (result)
            {
                case 0:
                    return Conflict();
                case 1:
                    return Ok();
                default:
                    return BadRequest();
            }
        }
    }
}
