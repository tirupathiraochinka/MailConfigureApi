using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailApiConfigure.Models
{
    public class XareltoModel
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string XareltoFirst { get; set; }
        public string ComplicationsOrSideeffects { get; set; }
        public string XareltoReason { get; set; }
        public string XareltoLovedOne { get; set; }
        public string XareltoComplicationsYesNo { get; set; }
        public string SignedDocuments { get; set; }
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