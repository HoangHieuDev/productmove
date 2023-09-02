using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMove_Model;
using SureSellOrganizers_API.Interfaces;

namespace SureSellOrganizers_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _repo;
        public ReportsController(IReportRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetReports")]
        public IActionResult GetReports()
        {
            try
            {
                var res = _repo.GetReports();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetReport/{id}")]
        public IActionResult GetReport(int id)
        {
            try
            {
                var res = _repo.GetReport(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddReport")]
        public IActionResult AddReport(Report Report)
        {
            try
            {
                _repo.AddReport(Report);
                return Ok("Add Report successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateReport/{id}")]
        public IActionResult UpdateReport(int id, Report Report)
        {
            try
            {
                _repo.UpdateReport(id, Report);
                return Ok("Update Report successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteReport/{id}")]
        public IActionResult DeleteReport(int id)
        {
            try
            {
                _repo.DeleteReport(id);
                return Ok("Delete Report with Report_id: " + id + " successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
