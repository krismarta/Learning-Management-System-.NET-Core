
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class SubCourseRepository : GeneralRepository<MyContext, SubCourse, int>
    {
        private readonly MyContext context;

        public SubCourseRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public List<SubCourse> GetbyCourse(int courseId)
        {
            var getbycourse = context.SubCourses.Where(k => k.Courseid == courseId).AsEnumerable();
            return getbycourse.ToList();
        }
    }
}
