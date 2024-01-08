using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace BizPilotBackEndProduction.Models
{
    public class Customers
    {
        [Key]
        public int CustId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
        public int LandNo { get; set; }
        public int MobileNo { get; set; }
        public string Email { get; set; }
        public int Fax { get; set; }
        public string ContactPesonName { get; set; }
        public int ContactPersonMobile { get; set; }
        public int ContactPersonLand { get; set; }

    }
}
