using BizPilotBackEndProduction.Models.Invoices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizPilotBackEndProduction.Repository.IRepository
{
    public interface IinvoiceRepository
    {
        Task<List<InvoiceHeader>> GetAllAsync(DbContext context); // Modify method signature
        Task<InvoiceHeader> GetByIdAsync(int id, DbContext context); // Modify method signature
        Task AddAsync(InvoiceHeader invoiceHeader, DbContext context); // Modify method signature
        Task UpdateAsync(InvoiceHeader invoiceHeader, DbContext context); // Modify method signature
        Task DeleteAsync(InvoiceHeader invoiceHeader, DbContext context); // Modify method signature
    }
}
