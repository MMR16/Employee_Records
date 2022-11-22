using Employee_Records_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Records_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepository _repository;

        public DepartmentController(IRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var depts = _repository.GetDepartments().ToList();
            if (depts.Count <= 0)
            {
                return NotFound();
            }
            return Ok(depts);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var dept = _repository.GetDepartment(id);
            if (dept is null)
            {
                return NotFound();
            }
            return Ok(dept);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var dept = _repository.GetDepartment(name);
            if (dept is null)
            {
                return NotFound();
            }
            return Ok(dept);
        }

        [HttpPost("{name}")]
        public IActionResult Add(string name)
        {

            var added = _repository.AddDepartment(name);
            if (!added)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Created("", null);
        }
    }
}
