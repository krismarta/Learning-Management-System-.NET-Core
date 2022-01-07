
using Microsoft.EntityFrameworkCore;
using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public int Login(RegisterVM registerVM)
        {
            try
            {
                var checkEmail = context.Users.Where(b => b.email == registerVM.email).FirstOrDefault();
                if (checkEmail != null)
                {
                    var password = (from e in context.Set<User>()
                                    where e.email == registerVM.email
                                    join a in context.Set<Account>() on e.id equals a.id
                                    select a.password).Single();


                    var checkPassword = Hashing.Hashing.ValidatePassword(registerVM.password, password);
                    //Password salah
                    if (checkPassword == false)
                    {
                        return 3;
                    }
                    //Login Berhasil
                    else
                    {
                        return 1;
                    }
                }
                //Email salah
                else
                {
                    return 2;
                }
            }
            catch
            {
                return 0;
            }
        }

        public ICollection GetProfile(string id)
        {
            var query = (from u in context.Set<User>()
                         join a in context.Set<Account>() on new { id = u.id } equals new { id = a.id }
                         join r in context.Set<Role>() on new { id = a.Roleid } equals new { id = r.id }
                         where u.id == id
                         select new
                         {
                             ID = u.id,
                             Email = u.email,
                             Name = u.name,
                             Gender = u.gender == 0 ? "Male" : "Female",
                             Phone = u.phone,
                             BirthDate = u.birthDate,
                             //PASSWORD HANYA UNTUK TESTING NANTI DIHAPUS
                             Password = a.password,
                             Role = r.name
                         }).AsEnumerable();
            var myList = query.ToList();
            return myList;
        }

        public int ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            string NIKEmail;
            try
            {
                NIKEmail = (from a in context.Users
                            where a.email == forgotPasswordVM.Email
                            join b in context.Accounts on a.id equals b.id
                            select b.id).Single();
            }
            catch (Exception)
            {

                return 2;

            }

            var account = context.Accounts.Find(NIKEmail);
            string uniqueString = Guid.NewGuid().ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append($"<h1> {uniqueString} <h1>");

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(forgotPasswordVM.Email);
                mail.From = new MailAddress("joantocourse.system@gmail.com", "Joanto Course", System.Text.Encoding.UTF8);
                DateTimeOffset now = (DateTimeOffset)DateTime.Now;
                mail.Subject = "Forgot Password " + now;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                //enkripsi base64 encode
                var emailEncodeBytes = System.Text.Encoding.UTF8.GetBytes(forgotPasswordVM.Email);
                var emailEncode = System.Convert.ToBase64String(emailEncodeBytes);

                //mail.Body = "<p>hai " + dataEmployee.FirstName + dataEmployee.LastName + "</p><p>ini adalah password baru kamu " +
                //    ": " + passwordnew + "</p>";

                

                mail.Body = "<p>&nbsp;</p> <table width='95%' cellspacing='0' cellpadding='0' align='center'> " +
                    "<tbody> <tr> <td align='center'> <table style='border-spacing: 2px 5px;' width='600'" +
                    " cellspacing='0' cellpadding='0' align='center' bgcolor='#fff'> <tbody> <tr> <td style='padding:" +
                    " 5px 5px 5px 5px;' align='center'><a href='https://localhost:44324/'><img src='https://i.ibb.co/kGZSjZp/header-logo-one.png'" +
                    " alt='header-logo-one' width='250px' border='0' /></a></td> </tr> <tr> <td bgcolor='#fff'> <table width='100%%'" +
                    " cellspacing='0' cellpadding='0'> <tbody> <tr> <td style='padding: 10px 0 10px 0; font-family: Nunito, sans-serif;" +
                    " font-size: 20px; font-weight: 900;'>Anda Mengubah Password Joanto Anda</td> </tr> </tbody> </table> </td> </tr> <tr>" +
                    " <td bgcolor='#fff'> <table style='height: 282px; width: 100%;' width='100%%' cellspacing='0' cellpadding='0'>" +
                    " <tbody> <tr style='height: 20px;'> <td style='padding: 20px 0px; font-family: Nunito, sans-serif; font-size: 16px;" +
                    " height: 20px;'>Hi, <span id='name'>" + forgotPasswordVM.Email.Split('@')[0] +
                    "</span></td> </tr> <tr style='height: 41px;'> " +
                    "<td style='padding: 0px; font-family: Nunito, sans-serif; font-size: 16px; height: 41px;'>" +
                    "Password baru anda : </td>"  +
                    sb.ToString() +
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
                account.password = Hashing.Hashing.HashPassword(uniqueString);
                context.SaveChanges();
               
                return 1;
                }
                catch (Exception)
                {
                    return 3;
                }

            
        }



        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var checkEmail = context.Users.Where(b => b.email == changePasswordVM.Email).FirstOrDefault();
            int result;
            //Tidak ada email
            if (checkEmail == null)
            {
                return 2;
            }
            else
            {
                var account = context.Accounts.Find(checkEmail.id);
                account.password = Hashing.Hashing.HashPassword(changePasswordVM.NewPassword);
                result = context.SaveChanges();
                return result;
            }
        }


        public int verifyAccount(string userid)
        {
            var result = 0;
            try
            {
                var entityAccount = context.Accounts.Where(ac => ac.id == userid).FirstOrDefault();
                entityAccount.Roleid = 2;
                context.SaveChanges();
                result = 1;
            }catch(Exception)
            {
                result = 0;
            }
            return result;
        }
    }
}
