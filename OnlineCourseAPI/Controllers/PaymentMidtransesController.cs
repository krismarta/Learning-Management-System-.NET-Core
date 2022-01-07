
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineCourseAPI.Base;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.Repository.Data;
using OnlineCourseAPI.ViewModel;
using System;
using System.IO;
using System.Net;

namespace OnlineCourseAPI.Controllers
{
    [Route("api/[controller]" )]
    [ApiController]
    public class PaymentMidtransesController : BaseController<PaymentMidtrans, PaymentMidtransRepository, int>
    {
        private readonly PaymentMidtransRepository paymentMidtransRepository;
        public PaymentMidtransesController(PaymentMidtransRepository paymentMidtransRepository) : base(paymentMidtransRepository)
        {
            this.paymentMidtransRepository = paymentMidtransRepository;
        }

        [HttpPost("GetPayment")]
        public ActionResult<MidtransVM> data(RequestPaymentVM requestPaymentVM)
        {
            var url = "https://app.sandbox.midtrans.com/snap/v1/transactions";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = "Basic U0ItTWlkLXNlcnZlci1laURob0wzTFJvQUxZVVgzMnBHd1pYUEY6";
            httpRequest.ContentType = "application/json";

            var datas = @"{
                ""transaction_details"": {
                    ""order_id"": ""_ORDERID_"",
                ""gross_amount"": ""_GROSS_""
                },
              ""item_details"": [
                {
                  ""id"": ""_ITEMID_"",
                  ""price"": ""_PRICE_"",
                  ""quantity"": 1,
                  ""name"": ""_TITLE_""
                }
              ],
              ""customer_details"": {
                ""first_name"": ""_NAME_"",
                ""email"": ""_EMAIL_"",
                ""phone"": ""_PHONE_""
              }
            }
            "
            .Replace("_ORDERID_", requestPaymentVM.Orderid)
            .Replace("_GROSS_", requestPaymentVM.gross_amount)
            .Replace("_ITEMID_", requestPaymentVM.Courseid)
            .Replace("_PRICE_", requestPaymentVM.Price)
            .Replace("_TITLE_", requestPaymentVM.Titlecourse)
            .Replace("_NAME_", requestPaymentVM.Namecustomer)
            .Replace("_EMAIL_", requestPaymentVM.Emailcustomer)
            .Replace("_PHONE_", requestPaymentVM.Teleponcustomer);


            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(datas);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var result = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            MidtransVM mi = JsonConvert.DeserializeObject<MidtransVM>(result);

            Console.WriteLine(httpResponse.StatusCode);

            return mi;
        }
        [HttpPost("AddPayment")]
        public IActionResult addPayment(ReqStatusPaymentVM reqStatusPaymentVM)
        {
            var result = paymentMidtransRepository.AddRequesttoDB(reqStatusPaymentVM);
            switch (result)
            {
                case 0:
                    return BadRequest();
                case 1:
                    return Ok();
                case 2:
                    return Accepted();
                default:
                    return BadRequest();
            }

        }

        [HttpPost("Callbackmidtrans")]
        public IActionResult Callbackmidtrans(CallbackMidtrans callbackMidtrans)
        {
            var result = paymentMidtransRepository.CallbackMidtrans(callbackMidtrans);
            switch (result)
            {
                case 0:
                    return BadRequest();
                case 1:
                    return Ok();
                default:
                    return BadRequest();
            }

        }


    }
}
