using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repo;
        public CustomersController(ICustomerRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers()
        {
            try
            {
                var res = _repo.GetCustomers();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomer/{id}")]
        public IActionResult GetCustomer(int id)
        {
            try
            {
                var res = _repo.GetCustomer(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(Customer Customer)
        {
            try
            {
                _repo.AddCustomer(Customer);
                return Ok("Add Customer successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCustomer/{id}")]
        public IActionResult UpdateCustomer(int id, Customer Customer)
        {
            try
            {
                _repo.UpdateCustomer(id, Customer);
                return Ok("Update Customer successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCustomer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                _repo.DeleteCustomer(id);
                return Ok("Delete Customer with Customer_id: " + id + " successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
