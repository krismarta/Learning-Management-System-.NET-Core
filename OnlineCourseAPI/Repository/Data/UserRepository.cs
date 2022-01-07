

using OnlineCourseAPI.Context;
using OnlineCourseAPI.Model;
using OnlineCourseAPI.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OnlineCourseAPI.Repository.Data
{
    public class UserRepository : GeneralRepository<MyContext, User, string>
    {
        private readonly MyContext context;
        public UserRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }


        public int Register(RegisterVM registerVM)
        {
            var result = 0;
            //  try
            // {
            UserNumber userNumber = new UserNumber();

            context.UserNumbers.Add(userNumber);
            result = context.SaveChanges();

            User user = new User()
                {
                    id= "usr"+userNumber.id,
                    name = registerVM.email.Split('@')[0],
                    email = registerVM.email,
                };

                var checkEmail = context.Users.Where(b => b.email == registerVM.email).FirstOrDefault();

                if (checkEmail != null)
                {
                    return 2;
                }
              
                context.Users.Add(user);
                result = context.SaveChanges();

                checkEmail = context.Users.Where(b => b.email == registerVM.email).FirstOrDefault();

                var userID = context.Users.Find(checkEmail.id);
                result = context.SaveChanges();

                Account account = new Account()
                {
                    id = userID.id,
                    password = registerVM.password,
                    Roleid = 3
                };
                string hashPassword = Hashing.Hashing.HashPassword(account.password);
                account.password = hashPassword;
                context.Accounts.Add(account);
                result = context.SaveChanges();

            //send email verifikasi account 
            if (result == 1)
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(registerVM.email);
                mail.From = new MailAddress("joantocourse.system@gmail.com", "Joanto Course", System.Text.Encoding.UTF8);
                DateTimeOffset now = (DateTimeOffset)DateTime.Now;
                mail.Subject = "Verifikasi New Account " + now;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                //enkripsi base64 encode
                var emailEncodeBytes = System.Text.Encoding.UTF8.GetBytes(registerVM.email+"|"+ "usr" + userNumber.id);
                var emailEncode =  System.Convert.ToBase64String(emailEncodeBytes);

                //mail.Body = "<p>hai " + dataEmployee.FirstName + dataEmployee.LastName + "</p><p>ini adalah password baru kamu " +
                //    ": " + passwordnew + "</p>";

                mail.Body = "<p>&nbsp;</p> <table width='95%' cellspacing='0' cellpadding='0' align='center'> " +
                    "<tbody> <tr> <td align='center'> <table style='border-spacing: 2px 5px;' width='600'" +
                    " cellspacing='0' cellpadding='0' align='center' bgcolor='#fff'> <tbody> <tr> <td style='padding:" +
                    " 5px 5px 5px 5px;' align='center'><a href='https://localhost:44324/'><img src='https://i.ibb.co/kGZSjZp/header-logo-one.png'" +
                    " alt='header-logo-one' width='250px' border='0' /></a></td> </tr> <tr> <td bgcolor='#fff'> <table width='100%%'" +
                    " cellspacing='0' cellpadding='0'> <tbody> <tr> <td style='padding: 10px 0 10px 0; font-family: Nunito, sans-serif;" +
                    " font-size: 20px; font-weight: 900;'>Activate Your Joanto Course Account</td> </tr> </tbody> </table> </td> </tr> <tr>" +
                    " <td bgcolor='#fff'> <table style='height: 282px; width: 100%;' width='100%%' cellspacing='0' cellpadding='0'>" +
                    " <tbody> <tr style='height: 20px;'> <td style='padding: 20px 0px; font-family: Nunito, sans-serif; font-size: 16px;" +
                    " height: 20px;'>Hi, <span id='name'>" + registerVM.email.Split('@')[0] +
                    "</span></td> </tr> <tr style='height: 41px;'> " +
                    "<td style='padding: 0px; font-family: Nunito, sans-serif; font-size: 16px; height: 41px;'>" +
                    "Thank you for registering at Joanto Course. Please confirm this email to activate your account.</td>" +
                    " </tr> <tr style='height: 54px;'> <td style='padding: 20px 0px; font-family: Nunito, sans-serif; font-size: 16px; " +
                    "text-align: center; height: 54px;'><a href='https://localhost:44324/verify/" + emailEncode +
                    "' style='padding: 8px 12px; border: 1px solid #ed948d;background-color:#ed948d;" +
                    "border-radius: 2px;font-family: Helvetica, Arial, sans-serif;font-size: 14px;" +
                    " color: #ffffff;text-decoration: none;font-weight:bold;display: inline-block;'>" +
                    " Confirm Email </a></td> </tr> <tr style='height: 94px;'>" +
                    " <td style='padding: 0px; font-family: Nunito, sans-serif;" +
                    " font-size: 16px; height: 94px;'>If you are having trouble clicking the" +
                    " 'Confirm Email',button, copy and paste the URL below into your browser: <p id='url'>https://localhost:44324/verify/" + emailEncode+
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
                    return 3;
                }

            }


            //}
            //catch
            //{
            //    return result;
            //}
            return result;
        }

        public int checkEmail(string email)
        {
            var result = 0;
            var checkEmail = context.Users.Where(b => b.email == email).FirstOrDefault();
            if (checkEmail != null)
            {
                var getRoleid = context.Users.Where(b => b.email == email).Select(User => User.Account.Roleid).ToList();
                if (getRoleid[0] == 3)
                {
                    result = 1;
                }
                else
                {
                    //ganto menjadi 0
                    result = 1;
                }
                
            }
            else
            {
                result = 0;
            }
            return result;
        }
        public IEnumerable GetRegister()
        {
            var query = (from u in context.Set<User>()
                         join a in context.Set<Account>() on new { id = u.id } equals new { id = a.id }
                         join r in context.Set<Role>() on new { id = a.Roleid } equals new { id = r.id }
                        
                         select new
                         {
                             ID = u.id,
                             Email = u.email,
                             Name = u.name,
                             Gender = u.gender == 0 ? "Male" :  "Female",
                             Phone = u.phone,
                             BirthDate = u.birthDate,
                             //PASSWORD HANYA UNTUK TESTING NANTI DIHAPUS
                             Password = a.password,
                             Role = r.name
                         }).AsEnumerable();
            var myList = query.ToList();
            return myList;
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

    }
}
