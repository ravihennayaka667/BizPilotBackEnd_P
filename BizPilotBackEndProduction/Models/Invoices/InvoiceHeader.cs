using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizPilotBackEndProduction.Models.Invoices
{
    public class InvoiceHeader
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


        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
