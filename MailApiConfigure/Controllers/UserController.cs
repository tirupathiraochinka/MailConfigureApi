using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MailApiConfigure.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

namespace MailApiConfigure.Controllers
{
    //[Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {

        [HttpPost]
        [Route("Register")]
        public bool Register(UserModel user)
        {
            bool isInserted = false;
            SqlConnection con = null;
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (con = new SqlConnection(strcon))
                {
                    string password = CreateRandomPassword(6);
                    con.Open();
                    string query = string.Format("insert into users(LoginName,Password,Name,EmailId,MobileNo,Status,IsActive) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", 
                        user.EmailId, password, user.Name,user.EmailId,user.MobileNo,2,true);
                    SqlCommand cmd = new SqlCommand(query, con);
                    int result = cmd.ExecuteNonQuery();
                    if (result>0)
                    {
                        string text = System.IO.File.ReadAllText((ConfigurationSettings.AppSettings["UserRegTemplat"]));
                        text = text.Replace("%username%", user.EmailId);
                        text = text.Replace("%password%", password);
                        SendEmail(text,user.EmailId);
                        isInserted = true;
                    }
                    con.Close();
                }

            }
            catch (Exception e)
            {
                con.Close();
            }
            return isInserted;
        }

        [HttpGet]
        [Route("GetUserList")]
        public List<UserModel> GetUserList()
        {
            List<UserModel> userlist =null;
            SqlConnection con = null;
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (con = new SqlConnection(strcon))
                {
                    con.Open();
                    string query = string.Format("select *  from users where status=1");
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    userlist = new List<UserModel>();
                    while (reader.Read())
                    {
                        UserModel  user = new UserModel();
                        int id;
                        Int32.TryParse(reader["Id"].ToString(), out id);
                        user.Id = id;
                        user.Name = reader["Name"].ToString();
                        user.EmailId = reader["EmailId"].ToString();
                        user.MobileNo = reader["MobileNo"].ToString();
                        int status;
                        Int32.TryParse(reader["Status"].ToString(), out status);
                        user.Status = status;
                        bool isactive;
                        Boolean.TryParse(reader["IsActive"].ToString(), out isactive);
                        user.IsActive = isactive;
                        //user.token = "QpwL5tke4Pnpja7X";
                        userlist.Add(user);
                    }
                    con.Close();
                }

            }
            catch (Exception e)
            {
                con.Close();
            }
            return userlist;
        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public void SendEmail(string text,string mailto)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(ConfigurationSettings.AppSettings["SendMailFrom"]);
                mail.To.Add(mailto);
                mail.Subject = ConfigurationSettings.AppSettings["UserRegSubject"];
                mail.Body = text;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment();
                using (SmtpClient smtp = new SmtpClient(ConfigurationSettings.AppSettings["SMTPEmail"], int.Parse(ConfigurationManager.AppSettings["SMTPPort"])))
                {
                    smtp.Credentials = new NetworkCredential(ConfigurationSettings.AppSettings["SendMailFrom"], ConfigurationSettings.AppSettings["SendMailPsw"]);
                    smtp.EnableSsl = bool.Parse(ConfigurationSettings.AppSettings["SSL"]);
                    smtp.Send(mail);
                }
            }
        }



    }
}
