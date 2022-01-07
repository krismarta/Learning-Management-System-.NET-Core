
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class PaymentRepository : GeneralRepository<MyContext, Payment, int>
    {
        private readonly MyContext context;
        public PaymentRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }
    }
}
