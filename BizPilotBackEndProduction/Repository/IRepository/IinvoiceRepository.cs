using BizPilotBackEndProduction.Models.Invoices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizPilotBackEndProduction.Repository.IRepository
{
    public interface IinvoiceRepository
    {
        Task<List<InvoiceHeader>> GetAllAsync();
        Task<InvoiceHeader> GetByIdAsync(int id);
        Task AddAsync(InvoiceHeader invoiceHeader);
        Task UpdateAsync(InvoiceHeader invoiceHeader);
        Task DeleteAsync(InvoiceHeader invoiceHeader);
    }
}
