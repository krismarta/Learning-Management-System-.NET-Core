
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class CategoryRepository : GeneralRepository<MyContext, Category, int>
    {
        private readonly MyContext context;
        public CategoryRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }
    }
}
