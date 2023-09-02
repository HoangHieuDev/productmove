using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExecutiveBoard_API.Interfaces;
using ProductMove_Model;

namespace ExecutiveBoard_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly ICategoryRepository _repo;
        public CategorysController(ICategoryRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetCategorys")]
        public IActionResult GetCategorys()
        {
            try
            {
                var res = _repo.GetCategorys();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCategory/{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var res = _repo.GetCategory(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(Category category)
        {
            try
            {
                _repo.AddCategory(category);
                return Ok("Add category successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCategory/{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {
            try
            {
                _repo.UpdateCategory(id, category);
                return Ok("Update category successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _repo.DeleteCategory(id);
                return Ok("Delete category with category_id: " + id + " successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
