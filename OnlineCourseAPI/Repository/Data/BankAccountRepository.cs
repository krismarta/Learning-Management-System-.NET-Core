
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class BankAccountRepository : GeneralRepository<MyContext, BankAccount, int>
    {
        private readonly MyContext context;
        public BankAccountRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public int checknoAccount(string noaccount)
        {
            var result = 0;
            var getNoaccount = context.BankAccounts.Where(b => b.no == noaccount).FirstOrDefault();

            if (getNoaccount != null)
            {
                result = 0;
            }
            else
            {
                result = 1;
            }
            return result;
        }

    }
}
