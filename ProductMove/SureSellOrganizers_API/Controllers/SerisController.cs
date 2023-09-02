using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerisController : ControllerBase
    {
        private readonly ISeriRepository _repo;
        public SerisController(ISeriRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetSeris")]
        public IActionResult GetSeris()
        {
            try
            {
                var res = _repo.GetSeries();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSeri/{id}")]
        public IActionResult GetSeri(int id)
        {
            try
            {
                var res = _repo.GetSeri(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddSeri")]
        public IActionResult AddSeri(Seri Seri)
        {
            try
            {
                _repo.AddSeri(Seri);
                return Ok("Add Seri successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateSeri/{id}")]
        public IActionResult UpdateSeri(int id, Seri Seri)
        {
            try
            {
                _repo.UpdateSeri(id, Seri);
                return Ok("Update Seri successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSeri/{id}")]
        public IActionResult DeleteSeri(int id)
        {
            try
            {
                _repo.DeleteSeri(id);
                return Ok("Delete Seri with Seri_id: " + id + " successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
