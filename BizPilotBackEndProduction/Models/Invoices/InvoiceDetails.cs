using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BizPilotBackEndProduction.Models.Invoices
{
    public class InvoiceDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int InvId { get; set; }
        public int ItemId { get; set; }
        public float NoOfUnits { get; set; }
        public float InvPrice { get; set; }
        public DateTime SysDate { get; set; }

        
    }
}
