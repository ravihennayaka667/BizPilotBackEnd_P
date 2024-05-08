using BizPilotBackEnd.Core.dbContext;
using BizPilotBackEndProduction.Models.Invoices;
using BizPilotBackEndProduction.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizPilotBackEndProduction.Repository
{
    public class InvoiceRepository : IinvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceHeader>> GetAllAsync()
        {
            return await _context.InvoiceHeaders
                .Include(invoice => invoice.InvoiceDetails)
                .ToListAsync();
        }

        public async Task<InvoiceHeader> GetByIdAsync(int id)
        {
            return await _context.InvoiceHeaders
                .Include(invoice => invoice.InvoiceDetails)
                .FirstOrDefaultAsync(invoice => invoice.InvId == id);
        }

        public async Task AddAsync(InvoiceHeader invoiceHeader)
        {
            _context.InvoiceHeaders.Add(invoiceHeader);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InvoiceHeader invoiceHeader)
        {
            _context.Entry(invoiceHeader).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(InvoiceHeader invoiceHeader)
        {
            // Remove related details
            _context.InvoiceDetails.RemoveRange(invoiceHeader.InvoiceDetails);

            // Remove header
            _context.InvoiceHeaders.Remove(invoiceHeader);

            await _context.SaveChangesAsync();
        }

    }
}
