using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BizPilotBackEndProduction.Models.Invoices
{
    public class InvoiceDetails
    {

        [Key]
        public int Id { get; set; }
        public int InvId { get; set; }
        public int ItemId { get; set; }
        public float NoOfUnits { get; set; }
        public float InvPrice { get; set; }
        public DateTime SysDate { get; set; }


        [ForeignKey("InvId")]

        public virtual ICollection<InvoiceHeader> InvoiceHeaders { get; set; }

    }
}
