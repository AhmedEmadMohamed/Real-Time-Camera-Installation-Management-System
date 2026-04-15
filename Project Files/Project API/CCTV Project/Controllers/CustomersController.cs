using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_Time_Camera_Installation_Management_System.Models.Domain;
using Real_Time_Camera_Installation_Management_System.Models.DTOs;
using Real_Time_Camera_Installation_Management_System.Repos.Interfaces;

namespace Real_Time_Camera_Installation_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomersController(ICustomerRepo customerRepo)
        {
            this._customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepo.GetAllAsync();
            var response = new List<ReadCustomerDTO>();
            foreach (var customer in customers)
            {
                response.Add(new ReadCustomerDTO
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    CustomerPhone = customer.CustomerPhone,
                    CustomerAddress = customer.CustomerAddress,
                    
                });
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDTO request)
        {
            var customer = new Customer
            {
                CustomerName = request.CustomerName,
                CustomerPhone = request.CustomerPhone,
                CustomerAddress = request.CustomerAddress
            };

            await _customerRepo.CreateAsync(customer);
            return Ok(new { message = "Customer Created Successfully" });
        }
    }
}
