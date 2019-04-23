using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailApiConfigure.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string EmailId{ get; set; }
        public string MobileNo{ get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public string token { get; set; }

    }
}