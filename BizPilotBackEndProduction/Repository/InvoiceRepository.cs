using BizPilotBackEndProduction.Models.Invoices;
using BizPilotBackEndProduction.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizPilotBackEndProduction.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public async Task<List<InvoiceHeader>> GetAllAsync(DbContext context)
        {
            return await context.Set<InvoiceHeader>()
                .Include(invoice => invoice.InvoiceDetails)
                .ToListAsync();
        }

        public async Task<InvoiceHeader> GetByIdAsync(int id, DbContext context)
        {
            return await context.Set<InvoiceHeader>()
                .Include(invoice => invoice.InvoiceDetails)
                .FirstOrDefaultAsync(invoice => invoice.InvId == id);
        }

        public async Task AddAsync(InvoiceHeader invoiceHeader, DbContext context)
        {
            context.Set<InvoiceHeader>().Add(invoiceHeader);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InvoiceHeader invoiceHeader, DbContext context)
        {
            context.Entry(invoiceHeader).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(InvoiceHeader invoiceHeader, DbContext context)
        {
            context.Set<InvoiceHeader>().Remove(invoiceHeader);
            await context.SaveChangesAsync();
        }
    }
}
