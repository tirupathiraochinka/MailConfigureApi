using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailApiConfigure.Models
{
    public class IVCModel
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string BloodClotFirst { get; set; }
        public string BrandNmaeIvcBloodClot { get; set; }
        public string ProofIvcBloodClot { get; set; }
        public string SurgeryRemoveIvc { get; set; }
        public string TempPermBloodClot { get; set; }
        public string SignedDocuments { get; set; }
        public string SufferedComplications { get; set; }
        public string HowOld { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Besttimetocontact { get; set; }
    }
}