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
    public class UsersController : BaseController<User, UserRepository, string>
    {
        private readonly UserRepository userrepository;
        public UsersController(UserRepository repository) : base(repository)
        {
            this.userrepository = repository;
        }


    }
}
