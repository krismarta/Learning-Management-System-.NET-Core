using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using OnlineCourseClient.Base.Controllers;
using OnlineCourseClient.Models;
using OnlineCourseClient.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseClient.Controllers
{
    public class PaymentController : BaseController<PaymentMidtrans, PaymentRepository, string>
    {
        private readonly PaymentRepository paymentRepository;
        public PaymentController(PaymentRepository repository) : base(repository)
        {
            this.paymentRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Payment(RequestPaymentVM entity)
        {
            var result = paymentRepository.Payment(entity);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Addpayment(ReqStatusPaymentVM entity)
        {
            var result = paymentRepository.Addpayment(entity);
            return Json(result);

        }

        [HttpPost]
        public JsonResult Callbackmidtrans(CallbackMidtrans callbackMidtrans)
        {
            var result = paymentRepository.Callbackmidtrans(callbackMidtrans);
            return Json(result);

        }


    }
}
