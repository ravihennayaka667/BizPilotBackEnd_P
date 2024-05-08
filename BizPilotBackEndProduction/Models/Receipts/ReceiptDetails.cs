using System.ComponentModel.DataAnnotations;

namespace BizPilotBackEndProduction.Models.Receipts
{
    public class ReceiptDetails
    {
        [Key]
        int ReceiptId { get; set; }
        string ReceiptCode { get; set; }
        int InvoiceNo { get; set; }
        int ReceiptType{ get; set; }
        float ReceiptAmount { get; set; }
        bool IsReceiptCancell { get; set; }
        int CancellUser { get; set; }


    }
}
