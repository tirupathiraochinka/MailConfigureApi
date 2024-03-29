﻿using MailApiConfigure.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace MailApiConfigure.Controllers
{
    [RoutePrefix("api/main")]
    public class MainController : ApiController
    {
        [HttpPost]
        [Route("PostInvokanObj")]
        public string PostInvokanObj(InvokanaModel model)
        {
            string result = string.Empty;
            try
            {
                model.ApplicantId=SaveApplicant(model.FullName, model.Email, model.PhoneNo, model.HowOld, model.Address, model.City, model.State, model.Zip, model.Besttimetocontact);
                if (model.ApplicantId>0)
                {
                    if (SaveInvokanaClaim(model))
                    {
                        string text = System.IO.File.ReadAllText((ConfigurationSettings.AppSettings["InvokanaTemplatePath"]));
                        text = text.Replace("%Diabetes2%", model.Diabetes2);
                        text = text.Replace("%InvokanaWhen%", model.InvokanaWhen);
                        text = text.Replace("%PrescribingInvokana%", model.PrescribingInvokana);
                        text = text.Replace("%PharmacyFilling%", model.PharmacyFilling);
                        text = text.Replace("%CeaseTakingInvokana%", model.CeaseTakingInvokana);
                        text = text.Replace("%Lowerextremityamputation%", model.Lowerextremityamputation);
                        text = text.Replace("%Identifiedinjury%", model.Identifiedinjury);
                        text = text.Replace("%Addressofthehospitals%", model.Addressofthehospitals);
                        text = text.Replace("%Within15dayuse%", model.Within15dayuse);
                        text = text.Replace("%Kidneyfailure%", model.Kidneyfailure);
                        text = text.Replace("%Lawfirmbefore%", model.Lawfirmbefore);
                        text = text.Replace("%HowOld%", model.HowOld);
                        text = text.Replace("%FullName%", model.FullName);
                        text = text.Replace("%Address%", model.Address);
                        text = text.Replace("%City%", model.City);
                        text = text.Replace("%State%", model.State);
                        text = text.Replace("%Zip%", model.Zip);
                        text = text.Replace("%PhoneNo%", model.PhoneNo);
                        text = text.Replace("%Email%", model.Email);
                        text = text.Replace("%Besttimetocontact%", model.Besttimetocontact);

                        SendEmail(text);
                        result = ConfigurationSettings.AppSettings["SuccessMsg"];
                    }
                }
                
                
            }
            catch (Exception e)
            {
                result = e.InnerException.ToString();
                SaveException(e);
            }
            return result;
        }


        [HttpPost]
        [Route("PostXareltoObj")]
        public string PostXareltoObj(XareltoModel model)
        {
            string result = string.Empty;
            try
            {
                string text = System.IO.File.ReadAllText((ConfigurationSettings.AppSettings["XareltoTemplatePath"]));
                text = text.Replace("%XareltoFirst%", model.XareltoFirst);
                text = text.Replace("%ComplicationsOrSideeffects%", model.ComplicationsOrSideeffects);
                text = text.Replace("%XareltoReason%", model.XareltoReason);
                text = text.Replace("%XareltoLovedOne%", model.XareltoLovedOne);
                text = text.Replace("%XareltoComplicationsYesNo%", model.XareltoComplicationsYesNo);
                text = text.Replace("%SignedDocuments%", model.SignedDocuments);
                text = text.Replace("%HowOld%", model.HowOld);
                text = text.Replace("%FullName%", model.FullName);
                text = text.Replace("%Address%", model.Address);
                text = text.Replace("%City%", model.City);
                text = text.Replace("%State%", model.State);
                text = text.Replace("%Zip%", model.Zip);
                text = text.Replace("%PhoneNo%", model.PhoneNo);
                text = text.Replace("%Email%", model.Email);
                text = text.Replace("%Besttimetocontact%", model.Besttimetocontact);

                SendEmail(text);
                result = ConfigurationSettings.AppSettings["SuccessMsg"];
            }
            catch (Exception e)
            {
                result = e.InnerException.ToString();
                SaveException(e);
            }
            return result;
        }

        [HttpPost]
        [Route("PostIvcObj")]
        public string PostIvcObj(IVCModel model)
        {
            string result = string.Empty;
            try
            {
                string text = System.IO.File.ReadAllText((ConfigurationSettings.AppSettings["IVCTemplatePath"]));
                text = text.Replace("%BloodClotFirst%", model.BloodClotFirst);
                text = text.Replace("%BrandNmaeIvcBloodClot%", model.BrandNmaeIvcBloodClot);
                text = text.Replace("%ProofIvcBloodClot%", model.ProofIvcBloodClot);
                text = text.Replace("%SurgeryRemoveIvc%", model.SurgeryRemoveIvc);
                text = text.Replace("%TempPermBloodClot%", model.TempPermBloodClot);
                text = text.Replace("%SignedDocuments%", model.SignedDocuments);
                text = text.Replace("%SufferedComplications%", model.SufferedComplications);
                text = text.Replace("%HowOld%", model.HowOld);
                text = text.Replace("%FullName%", model.FullName);
                text = text.Replace("%Address%", model.Address);
                text = text.Replace("%City%", model.City);
                text = text.Replace("%State%", model.State);
                text = text.Replace("%Zip%", model.Zip);
                text = text.Replace("%PhoneNo%", model.PhoneNo);
                text = text.Replace("%Email%", model.Email);
                text = text.Replace("%Besttimetocontact%", model.Besttimetocontact);

                SendEmail(text);
                result = ConfigurationSettings.AppSettings["SuccessMsg"];
            }
            catch (Exception e)
            {
                result = e.InnerException.ToString();
                SaveException(e);
            }
            return result;
        }

        public void SaveException(Exception e)
        {
            Console.Write(e.Message.ToString());
            using (StreamWriter sw = File.AppendText(ConfigurationSettings.AppSettings["ExceptionWriter"]))
            {
                string error = "Log Written Date:" + " " + DateTime.Now.ToString();
                sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                sw.WriteLine("------------------------------ Inner Exception -------------------------------------------------------");
                sw.WriteLine("Inner Exeption Message:" + " " + e.InnerException.ToString());
                sw.WriteLine("--------------------------------*Message*------------------------------------------");
                sw.WriteLine("Exception Message:" + " " + e.Message.ToString());
                sw.WriteLine("--------------------------------*End*------------------------------------------");
                sw.Flush();
                sw.Close();

            }
        }
        // GET: api/Main
        public string Get()
        {
            string result = string.Empty;
            try
            {
                string text = System.IO.File.ReadAllText((ConfigurationSettings.AppSettings["TemplatePath"]));
                SendEmail(text);
                result = ConfigurationSettings.AppSettings["SuccessMsg"];
            }
            catch (Exception e)
            {
                result = e.Message.ToString();
                Console.Write(e.Message.ToString());
            }
            return result;
        }

        public void SendEmail(string text)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(ConfigurationSettings.AppSettings["SendMailFrom"]);
                mail.To.Add(ConfigurationSettings.AppSettings["MailTo"]);
                mail.Subject = ConfigurationSettings.AppSettings["MailSubject"];
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


        [HttpPost]
        [Route("Login")]
        public UserModel Login(LoginModel login)
        {
            UserModel user = null;
            SqlConnection con = null;
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (con = new SqlConnection(strcon))
                {
                    con.Open();
                    string query = string.Format("select * from users where loginname='{0}' and password='{1}'",login.Username,login.Password);
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader=cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user = new UserModel();
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
                        user.token = "QpwL5tke4Pnpja7X";
                    }
                    con.Close();
                }

            }
            catch (Exception e)
            {
                con.Close();
            }
            return user;
        }

        public int SaveApplicant(string name, string email, string phoneno, string dob, string address, string city, string state, string zip, string bestttc)
        {
            int applicantId = 0;
            SqlConnection con = null;
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (con = new SqlConnection(strcon))
                {
                    con.Open();
                    string query = string.Format("insert into Applicant(Name,Email,PhoneNo,DOB,Address,City,State,Zip,BestTTC,Status,IsActive) output INSERTED.ID values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                        name,email,phoneno,dob, address, city,state,zip,bestttc,1, true);
                    SqlCommand cmd = new SqlCommand(query, con);
                    applicantId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }

            }
            catch (Exception e)
            {
                applicantId = 0;
                con.Close();
            }
            return applicantId;
        }

        public bool SaveInvokanaClaim(InvokanaModel m)
        {
            bool isInserted = false;
            SqlConnection con = null;
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (con = new SqlConnection(strcon))
                {
                    con.Open();
                    string query = string.Format("insert into InvokanaClaim(ApplicantId,Diabetes2,InvokanaWhen,PrescribingInvokana,PharmacyFilling,CeaseTakingInvokana,"+
                        "Lowerextremityamputation,Identifiedinjury,Addressofthehospitals,Within15dayuse,Kidneyfailure,Lawfirmbefore,IsActive) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                        m.ApplicantId,m.Diabetes2,m.InvokanaWhen,m.PrescribingInvokana,m.PharmacyFilling,m.CeaseTakingInvokana,m.Lowerextremityamputation,m.Identifiedinjury,m.Addressofthehospitals,
                        m.Within15dayuse,m.Kidneyfailure,m.Lawfirmbefore,true);
                    SqlCommand cmd = new SqlCommand(query, con);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        isInserted = true;

                    con.Close();
                }

            }
            catch (Exception e)
            {
                con.Close();
            }
            return isInserted;
        }

        //public bool SaveXaralto(XareltoModel m)
        //{
        //    bool isInserted = false;
        //    SqlConnection con = null;
        //    try
        //    {
        //        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        //        using (con = new SqlConnection(strcon))
        //        {
        //            con.Open();
        //            string query = string.Format("insert into XareltoClaim(Diabetes2,InvokanaWhen,PrescribingInvokana,PharmacyFilling,CeaseTakingInvokana," +
        //                "Lowerextremityamputation,Identifiedinjury,Addressofthehospitals,Within15dayuse,Kidneyfailure,Lawfirmbefore,IsActive) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
        //                m.Diabetes2, m.InvokanaWhen, m.PrescribingInvokana, m.PharmacyFilling, m.CeaseTakingInvokana, m.Lowerextremityamputation, m.Identifiedinjury, m.Addressofthehospitals,
        //                m.Within15dayuse, m.Kidneyfailure, m.Lawfirmbefore, true);
        //            SqlCommand cmd = new SqlCommand(query, con);
        //            int result = cmd.ExecuteNonQuery();
        //            if (result > 0)
        //                isInserted = true;

        //            con.Close();
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        con.Close();
        //    }
        //    return isInserted;
        //}
    }
}
