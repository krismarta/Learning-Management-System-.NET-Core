
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class UserNumberRepository : GeneralRepository<MyContext, UserNumber, int>
    {
        private readonly MyContext context;
        public UserNumberRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }
    }
}
