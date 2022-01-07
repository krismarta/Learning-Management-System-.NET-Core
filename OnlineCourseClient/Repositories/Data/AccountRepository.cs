using Microsoft.AspNetCore.Http;
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
    public class AccountRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public AccountRepository(Address address, string request = "Accounts/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };

            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));

        }

        public HttpStatusCode Forgot(ForgotPasswordVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            string myContent = content.ReadAsStringAsync().Result;
            var result = httpClient.PostAsync(address.Link + request + "Forgot", content).Result;
            return result.StatusCode;
        }
        public HttpStatusCode ChangePassword(ChangePasswordVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            string myContent = content.ReadAsStringAsync().Result;
            var result = httpClient.PutAsync(address.Link + request + "change", content).Result;
            return result.StatusCode;
        }


        public async Task<JWTokenVM> Auth(RegisterVM login)
        {
            JWTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "Login", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

            return token;
        }

        public HttpStatusCode UpdateRL(string userid)
        {
            var result = httpClient.GetAsync(request + "verifyAccount/" +userid).Result;
            return result.StatusCode;
        }

    }
}