using CarService3.BL.Interfaces;
using CarService3.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarService3.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet(nameof(GetAllCustomers))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllCustomers()
        {
            var customers = 
                _customerService.GetAll();

            if (customers?.Count == 0) return NoContent();
            
            return Ok(customers);
        }

        [HttpGet(nameof(GetCustomerById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCustomerById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be greater than zero.");
            }

            var customer =
                _customerService.GetById(id);

            if (customer == null) return NotFound();

            return Ok(customer);
        }

        [HttpPost(nameof(AddCustomer))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCustomer([FromBody] Customer? customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer cannot be null.");
            }

            _customerService.Add(customer);

            return Ok();
        }

        [HttpDelete(nameof(DeleteCustomer))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be greater than zero.");
            }

            _customerService.Delete(id);

            return Ok();
        }
    }
}
