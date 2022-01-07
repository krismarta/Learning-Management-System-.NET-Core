
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class SubCourseLogRepository : GeneralRepository<MyContext, SubCourseLog, int>
    {
        private readonly MyContext context;
        public SubCourseLogRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }
    }
}
