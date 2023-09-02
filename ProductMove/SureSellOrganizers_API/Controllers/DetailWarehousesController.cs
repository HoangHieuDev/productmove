using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailWarehousesController : ControllerBase
    {
        private readonly IDetailWarehouseRepository _repo;
        public DetailWarehousesController(IDetailWarehouseRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetDetailWarehouses")]
        public IActionResult GetDetailWarehouses()
        {
            try
            {
                var res = _repo.GetDetailWarehouses();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDetailWarehouse/{id}")]
        public IActionResult GetDetailWarehouse(int id)
        {
            try
            {
                var res = _repo.GetDetailWarehouse(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddDetailWarehouse")]
        public IActionResult AddDetailWarehouse(DetailWarehouse DetailWarehouse)
        {
            try
            {
                _repo.AddDetailWarehouse(DetailWarehouse);
                return Ok("Add DetailWarehouse successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateDetailWarehouse/{id}")]
        public IActionResult UpdateDetailWarehouse(int id, DetailWarehouse DetailWarehouse)
        {
            try
            {
                _repo.UpdateDetailWarehouse(id, DetailWarehouse);
                return Ok("Update DetailWarehouse successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteDetailWarehouse/{id}")]
        public IActionResult DeleteDetailWarehouse(int id)
        {
            try
            {
                _repo.DeleteDetailWarehouse(id);
                return Ok("Delete DetailWarehouse with DetailWarehouse_id: " + id + " successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
