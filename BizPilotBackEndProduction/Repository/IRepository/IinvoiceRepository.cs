using BizPilotBackEndProduction.Models.Invoices;

namespace BizPilotBackEndProduction.Repository.IRepository
{
    public interface IinvoiceRepository
    {

        Task<List<InvoiceHeader>> GetAllAsync();
    }
}
