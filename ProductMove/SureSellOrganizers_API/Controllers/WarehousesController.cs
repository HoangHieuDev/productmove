using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseRepository _repo;
        public WarehousesController(IWarehouseRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetWarehouses")]
        public IActionResult GetWarehouses()
        {
            try
            {
                var res = _repo.GetWarehouses();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetWarehouse/{id}")]
        public IActionResult GetWarehouse(int id)
        {
            try
            {
                var res = _repo.GetWarehouse(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddWarehouse")]
        public IActionResult AddWarehouse(Warehouse Warehouse)
        {
            try
            {
                _repo.AddWarehouse(Warehouse);
                return Ok("Add Warehouse successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateWarehouse/{id}")]
        public IActionResult UpdateWarehouse(int id, Warehouse Warehouse)
        {
            try
            {
                _repo.UpdateWarehouse(id, Warehouse);
                return Ok("Update Warehouse successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteWarehouse/{id}")]
        public IActionResult DeleteWarehouse(int id)
        {
            try
            {
                _repo.DeleteWarehouse(id);
                return Ok("Delete Warehouse with Warehouse_id: " + id + " successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
