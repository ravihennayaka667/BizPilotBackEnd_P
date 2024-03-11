using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizPilotBackEndProduction.Models
{
    public class Invoiceh
    {
        [Key]
        public int InvId { get; set; }
        public string InvCode { get; set; }
        public DateTime InvDate { get; set; }

        public int CustId { get; set; }
        public int Discount { get; set; }
        public float DisAmount { get; set; }
        public int UserId { get; set; }
        public float TotalAmount { get; set; }
        public float GrossInvAmount { get; set; }
        public bool isCancell { get; set; }
        public int CancellUser { get; set; }


        public virtual ICollection<Invoiced> InvoiceDetails { get; set; }

    }

    public class Invoiced
    {
        [Key]
        public int InvId { get; set; }
        public int ItemId { get; set; }
        public float NoOfUnits { get; set; }
        public float InvPrice { get; set; }
        public DateTime SysDate { get; set; }


        [ForeignKey("InvId")]

        public virtual ICollection<Invoiced> InvoiceHeaders { get; set; }




    }

}
