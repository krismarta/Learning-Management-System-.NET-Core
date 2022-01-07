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
    public class PaymentRepository : GeneralRepository<PaymentMidtrans, string>
    {
        private readonly Address address;
        private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public PaymentRepository(Address address, string request = "PaymentMidtranses/") : base(address, request)
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

        public async Task<MidtransVM> Payment(RequestPaymentVM entity)
        {

            MidtransVM entitys = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "GetPayment", content).Result;
            string apiResponse = await result.Content.ReadAsStringAsync();
            entitys = JsonConvert.DeserializeObject<MidtransVM>(apiResponse);
            return entitys;
        }

        public HttpStatusCode Addpayment(ReqStatusPaymentVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            string myContent = content.ReadAsStringAsync().Result;
            var result = httpClient.PostAsync(address.Link + request + "AddPayment", content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode Callbackmidtrans(CallbackMidtrans entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            string myContent = content.ReadAsStringAsync().Result;
            var result = httpClient.PostAsync(address.Link + request + "Callbackmidtrans", content).Result;
            return result.StatusCode;
        }

    }
}
