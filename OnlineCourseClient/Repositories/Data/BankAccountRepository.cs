using Newtonsoft.Json;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using OnlineCourseClient.Base.Urls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseClient.Repositories.Data
{
    public class BankAccountRepository : GeneralRepository<BankAccount, int>
    {
        private readonly Address address;
        private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public BankAccountRepository(Address address, string request = "bankAccounts/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            //_contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));

        }

        //public HttpStatusCode RegisterPost(RegisterVM entity)
        //{
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        //    string myContent = content.ReadAsStringAsync().Result;
        //    var result = httpClient.PostAsync(address.Link + request + "Register", content).Result;
        //    return result.StatusCode;
        //}

        public HttpStatusCode checknoAccount(string no)
        {
            var result = httpClient.GetAsync(request +"check/" + no).Result;
            return result.StatusCode;
        }

    }
}
