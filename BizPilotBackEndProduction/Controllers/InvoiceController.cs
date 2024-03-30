using BizPilotBackEnd.Core.dbContext;
using BizPilotBackEndProduction.Models.Invoices;
using BizPilotBackEndProduction.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizPilotBackEndProduction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public InvoiceController(IInvoiceRepository invoiceRepository, IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _invoiceRepository = invoiceRepository;
            _contextFactory = contextFactory;
        }

        // GET: api/invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceHeader>>> GetAllInvoices()
        {
            using var context = _contextFactory.CreateDbContext();
            var invoices = await _invoiceRepository.GetAllAsync(context);
            return Ok(invoices);
        }

        // GET: api/invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceHeader>> GetInvoice(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var invoice = await _invoiceRepository.GetByIdAsync(id, context);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        // POST: api/invoice
        [HttpPost]
        public async Task<ActionResult<InvoiceHeader>> CreateInvoice(InvoiceHeader invoiceHeader)
        {
            using var context = _contextFactory.CreateDbContext();
            await _invoiceRepository.AddAsync(invoiceHeader, context);
            return CreatedAtAction(nameof(GetInvoice), new { id = invoiceHeader.InvId }, invoiceHeader);
        }

        // PUT: api/invoice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, InvoiceHeader invoiceHeader)
        {
            if (id != invoiceHeader.InvId)
            {
                return BadRequest();
            }

            using var context = _contextFactory.CreateDbContext();
            await _invoiceRepository.UpdateAsync(invoiceHeader, context);

            return NoContent();
        }

        // DELETE: api/invoice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var invoice = await _invoiceRepository.GetByIdAsync(id, context);
            if (invoice == null)
            {
                return NotFound();
            }

            await _invoiceRepository.DeleteAsync(invoice, context);

            return NoContent();
        }
    }
}
