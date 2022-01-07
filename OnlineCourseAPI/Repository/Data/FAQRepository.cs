
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class FAQRepository : GeneralRepository<MyContext, FAQ, int>
    {
        private readonly MyContext context;
        public FAQRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }
    }
}
