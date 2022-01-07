
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class MyCourseRepository : GeneralRepository<MyContext, MyCourse, int>
    {
        private readonly MyContext context;
        public MyCourseRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public ICollection GetUserCourse(string id)
        {
            var query = (from u in context.Set<User>()
                         join my in context.Set<MyCourse>() on new { id = u.id } equals new { id = my.Userid }
                         join c in context.Set<Course>() on new { id = my.Courseid } equals new { id = c.id }
                         join ca in context.Set<Category>() on new { id = c.Categoryid } equals new { id = ca.id }
                         where my.Userid == id
                         select new
                         {
                             MyCouseId = my.id,
                             UserId = u.id,
                             UserName = u.name,
                             CategoryId = ca.id,
                             CategoryName = ca.name,
                             CourseId = c.id,
                             CourseTitle = c.title,
                             CoursePrice =c.price,
                             CourseThumbnail = c.thumbnail,
                             CourseDescription = c.description
                         }).AsEnumerable();
            var myList = query.ToList();
            return myList;
        }



    }
}
