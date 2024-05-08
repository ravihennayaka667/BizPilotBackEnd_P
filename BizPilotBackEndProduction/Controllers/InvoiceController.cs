using BizPilotBackEndProduction.Models.Invoices;
using BizPilotBackEndProduction.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizPilotBackEndProduction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IinvoiceRepository _invoiceRepository;

        public InvoiceController(IinvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceHeader>>> GetInvoices()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return Ok(invoices);
        }

        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceHeader>> GetInvoice(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }



        // POST: api/Invoice
        [HttpPost]
        public async Task<ActionResult<InvoiceHeader>> PostInvoice(InvoiceHeader invoiceHeader)
        {
            await _invoiceRepository.AddAsync(invoiceHeader);
            return CreatedAtAction(nameof(GetInvoice), new { id = invoiceHeader.InvId }, invoiceHeader);
        }




        // PUT: api/Invoice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, InvoiceHeader invoiceHeader)
        {
            if (id != invoiceHeader.InvId)
            {
                return BadRequest();
            }
            try
            {
                await _invoiceRepository.UpdateAsync(invoiceHeader);
            }
            catch (Exception)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        // DELETE: api/Invoice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            try
            {
                await _invoiceRepository.DeleteAsync(invoice);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while deleting the invoice.");
            }
        }


        private bool InvoiceExists(int id)
        {
            // Implement this method based on your application logic
            throw new NotImplementedException();
        }
    }
}



