using CarService3.BL.Interfaces;
using CarService3.Host.Validators;
using CarService3.Models.Entities;
using CarService3.Models.Requests;
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CarService3.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<AddCustomerRequest> _validator;

        public CustomerController(
            ICustomerService customerService,
            ILogger<CustomerController> logger,
            IMapper mapper,
            IValidator<AddCustomerRequest> validator)
        {
            _customerService = customerService;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet(nameof(GetAllCustomers))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAll();

            if (customers.Count == 0) return NoContent();

            return Ok(customers);
        }

        [HttpGet(nameof(GetCustomerById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id must be greater than zero.");
            }

            var customer = await _customerService.GetById(id);

            if (customer == null) return NotFound();

            return Ok(customer);
        }

        [HttpPost(nameof(AddCustomer))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerRequest? request)
        {
            if (request == null)
            {
                return BadRequest("Customer cannot be null.");
            }

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var customer = _mapper.Map<Customer>(request);

            if (customer == null) return BadRequest("Mapping failed.");

            _ = _customerService.Add(customer);

           return Ok();
        }

        [HttpDelete(nameof(DeleteCustomer))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id must be greater than zero.");
            }

            await _customerService.Delete(id);

            return Ok();
        }
    }
}
