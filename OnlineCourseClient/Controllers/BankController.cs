using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using OnlineCourseClient.Base.Controllers;
using OnlineCourseClient.Models;
using OnlineCourseClient.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseClient.Controllers
{
    public class BankController : BaseController<BankAccount, BankAccountRepository, int>
    {
        private readonly BankAccountRepository bankAccountRepository;
        public BankController(BankAccountRepository repository) : base(repository)
        {
            this.bankAccountRepository = repository;
        }


    }
}
