using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportsController : ControllerBase
    {
        private readonly IImportRepository _repo;
        public ImportsController(IImportRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetImports")]
        public IActionResult GetImports()
        {
            try
            {
                var res = _repo.GetImports();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetImport/{id}")]
        public IActionResult GetImport(int id)
        {
            try
            {
                var res = _repo.GetImport(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddImport")]
        public IActionResult AddImport(Import Import)
        {
            try
            {
                _repo.AddImport(Import);
                return Ok("Add Import successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateImport/{id}")]
        public IActionResult UpdateImport(int id, Import Import)
        {
            try
            {
                _repo.UpdateImport(id, Import);
                return Ok("Update Import successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteImport/{id}")]
        public IActionResult DeleteImport(int id)
        {
            try
            {
                _repo.DeleteImport(id);
                return Ok("Delete Import with Import_id: " + id + " successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
