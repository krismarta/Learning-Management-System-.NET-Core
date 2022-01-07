
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class CourseRepository : GeneralRepository<MyContext, Course, int>
    {
        private readonly MyContext context;
        public CourseRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        


    }
}
