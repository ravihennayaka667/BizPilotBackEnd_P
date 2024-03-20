using BizPilotBackEnd.Core.dbContext;
using BizPilotBackEndProduction.Models.Invoices;
using BizPilotBackEndProduction.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BizPilotBackEndProduction.Repository
{
    public class InvoiceRepository : IinvoiceRepository
    {

        private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context) {

            _context = context;
        
        }

        public Task<List<InvoiceHeader>> GetAllAsync()
        {
            return _context.InvoiceHeaders.ToListAsync();
        }
    }
}
