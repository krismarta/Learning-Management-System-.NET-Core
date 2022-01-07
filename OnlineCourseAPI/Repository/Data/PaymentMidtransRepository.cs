
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class PaymentMidtransRepository : GeneralRepository<MyContext, PaymentMidtrans, int>
    {
        private readonly MyContext context;
        public PaymentMidtransRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }
        public string GetPayment(RequestPaymentVM requestPaymentVM)
        {
            List<MidtransVM> getallcourseVM = new List<MidtransVM>();
            MidtransVM selectCourse = new MidtransVM();

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

            Console.WriteLine(httpResponse.StatusCode);

            return result;
        }

        public int AddRequesttoDB(ReqStatusPaymentVM reqStatusPaymentVM)
        {
            var result = 0;
            //check status
            if (reqStatusPaymentVM.status == "settlement")
            {
                PaymentMidtrans payOK = new PaymentMidtrans()
                {
                    id = reqStatusPaymentVM.Orderid,
                    method = reqStatusPaymentVM.Method,
                    price = reqStatusPaymentVM.Price,
                    VA = reqStatusPaymentVM.VANumber,
                    status = reqStatusPaymentVM.status,
                    Userid = reqStatusPaymentVM.Userid,
                    Courseid = reqStatusPaymentVM.Courseid
                };
                context.PaymentMidtranses.Add(payOK);

                result = context.SaveChanges();
                if (result == 1)
                {
                    MyCourse courseOK = new MyCourse()
                    {
                        Courseid = reqStatusPaymentVM.Courseid,
                        Userid = reqStatusPaymentVM.Userid
                    };
                    context.MyCourses.Add(courseOK);
                    context.SaveChanges();
                    result = 1;
                }
                else
                {
                    result = 0;
                }
            }
            else 
            {
                PaymentMidtrans payPending = new PaymentMidtrans()
                {
                    id = reqStatusPaymentVM.Orderid,
                    method = reqStatusPaymentVM.Method,
                    price = reqStatusPaymentVM.Price,
                    VA = reqStatusPaymentVM.VANumber,
                    status = reqStatusPaymentVM.status,
                    Userid = reqStatusPaymentVM.Userid,
                    Courseid = reqStatusPaymentVM.Courseid
                };
                context.PaymentMidtranses.Add(payPending);
                context.SaveChanges();
                result = 2;
            }
            return result;
        }

        public int CallbackMidtrans(CallbackMidtrans callbackMidtrans)
        {
            var result = 0;
            var findOrderid = context.PaymentMidtranses.Where(r => r.id == callbackMidtrans.order_id).FirstOrDefault();

            if (findOrderid != null)
            {
                //check status payment
                if (callbackMidtrans.transaction_status == "settlement")
                {
                    //update status payment
                    findOrderid.status = "settlement";
                    context.SaveChanges();
                    result = 1;
                    if (result == 1)
                    {
                        MyCourse courseOK = new MyCourse()
                        {
                            Courseid = findOrderid.Courseid,
                            Userid = findOrderid.Userid
                        };
                        context.MyCourses.Add(courseOK);
                        context.SaveChanges();

                        //send email notification
                        var getemail = context.Users.Where(r => r.id == findOrderid.Userid).FirstOrDefault();

                        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                        mail.To.Add(getemail.email);
                        mail.From = new MailAddress("joantocourse.system@gmail.com", "Payment Accepted", System.Text.Encoding.UTF8);
                        DateTimeOffset now = (DateTimeOffset)DateTime.Now;
                        mail.Subject = "Payment Accepted " + now;
                        mail.SubjectEncoding = System.Text.Encoding.UTF8;
                      

                        //mail.Body = "<p>hai " + dataEmployee.FirstName + dataEmployee.LastName + "</p><p>ini adalah password baru kamu " +
                        //    ": " + passwordnew + "</p>";

                        mail.Body = "<p>&nbsp;</p> <table width='95%' cellspacing='0' cellpadding='0' align='center'> " +
                            "<tbody> <tr> <td align='center'> <table style='border-spacing: 2px 5px;' width='600'" +
                            " cellspacing='0' cellpadding='0' align='center' bgcolor='#fff'> <tbody> <tr> <td style='padding:" +
                            " 5px 5px 5px 5px;' align='center'><a href='https://localhost:44324/'><img src='https://i.ibb.co/kGZSjZp/header-logo-one.png'" +
                            " alt='header-logo-one' width='250px' border='0' /></a></td> </tr> <tr> <td bgcolor='#fff'> <table width='100%%'" +
                            " cellspacing='0' cellpadding='0'> <tbody> <tr> <td style='padding: 10px 0 10px 0; font-family: Nunito, sans-serif;" +
                            " font-size: 20px; font-weight: 900;'>Your Payment Successful</td> </tr> </tbody> </table> </td> </tr> <tr>" +
                            " <td bgcolor='#fff'> <table style='height: 282px; width: 100%;' width='100%%' cellspacing='0' cellpadding='0'>" +
                            " <tbody> <tr style='height: 20px;'> <td style='padding: 20px 0px; font-family: Nunito, sans-serif; font-size: 16px;" +
                            " height: 20px;'>Hi, <span id='name'>" + getemail.email.Split('@')[0] +
                            "</span></td> </tr> <tr style='height: 41px;'> " +
                            "<td style='padding: 0px; font-family: Nunito, sans-serif; font-size: 16px; height: 41px;'>" +
                            "You can already access the course you bought in the 'My Course' menu or click the button below.</td>" +
                            " </tr> <tr style='height: 54px;'> <td style='padding: 20px 0px; font-family: Nunito, sans-serif; font-size: 16px; " +
                            "text-align: center; height: 54px;'><a href='https://localhost:44324/main/"+
                            "' style='padding: 8px 12px; border: 1px solid #ed948d;background-color:#ed948d;" +
                            "border-radius: 2px;font-family: Helvetica, Arial, sans-serif;font-size: 14px;" +
                            " color: #ffffff;text-decoration: none;font-weight:bold;display: inline-block;'>" +
                            " Visit Website Joanto Course </a></td> </tr> <tr style='height: 94px;'>" +
                            " <td style='padding: 0px; font-family: Nunito, sans-serif;" +
                            " font-size: 16px; height: 94px;'>If you are having trouble clicking the" +
                            " 'Visit Webstie Joanto Course',button, copy and paste the URL below into your browser: <p id='url'>https://localhost:44324/main/" +
                            "</p> </td> </tr> " +
                            "<tr style='height: 73px;'> <td style='padding: 50px 0px; font-family: Nunito, sans-serif; font-size: 16px; height:" +
                            " 73px;'>System, <p>JoantoCourse</p> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table>";
                        mail.BodyEncoding = System.Text.Encoding.UTF8;
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.High;
                        SmtpClient client = new SmtpClient();
                        client.Credentials = new System.Net.NetworkCredential("joantocourse.system@gmail.com", "joanto123456789");
                        client.Port = 587;
                        client.Host = "smtp.gmail.com";
                        client.EnableSsl = true;
                        try
                        {
                            client.Send(mail);
                            return 1;
                        }
                        catch (Exception)
                        {
                            return 1;
                        }
                    }

                }

            }

            return 0;
        }

    }
}
