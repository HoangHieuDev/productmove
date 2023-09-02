using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportsController : ControllerBase
    {
        private readonly IExportRepository _repo;
        public ExportsController(IExportRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetExports")]
        public IActionResult GetExports()
        {
            try
            {
                var res = _repo.GetExports();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetExport/{id}")]
        public IActionResult GetExport(int id)
        {
            try
            {
                var res = _repo.GetExport(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddExport")]
        public IActionResult AddExport(Export Export)
        {
            try
            {
                _repo.AddExport(Export);
                return Ok("Add Export successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateExport/{id}")]
        public IActionResult UpdateExport(int id, Export Export)
        {
            try
            {
                _repo.UpdateExport(id, Export);
                return Ok("Update Export successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteExport/{id}")]
        public IActionResult DeleteExport(int id)
        {
            try
            {
                _repo.DeleteExport(id);
                return Ok("Delete Export with Export_id: " + id + " successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
