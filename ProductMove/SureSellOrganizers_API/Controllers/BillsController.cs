using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillRepository _repo;
        public BillsController(IBillRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetBills")]
        public IActionResult GetBills()
        {
            try
            {
                var res = _repo.GetBills();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetBill/{id}")]
        public IActionResult GetBill(int id)
        {
            try
            {
                var res = _repo.GetBill(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddBill")]
        public IActionResult AddBill(Bill Bill)
        {
            try
            {
                _repo.AddBill(Bill);
                return Ok("Add Bill successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateBill/{id}")]
        public IActionResult UpdateBill(int id, Bill Bill)
        {
            try
            {
                _repo.UpdateBill(id, Bill);
                return Ok("Update Bill successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteBill/{id}")]
        public IActionResult DeleteBill(int id)
        {
            try
            {
                _repo.DeleteBill(id);
                return Ok("Delete Bill with Bill_id: " + id + " successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
