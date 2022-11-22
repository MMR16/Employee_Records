using Employee_Records_API.DTO;
using Employee_Records_API.Interfaces;
using Employee_Records_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Records_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository _repository;

        public EmployeeController(IRepository repository)
        {
            _repository = repository;
        }


        [HttpGet()]
        public ActionResult<List<Employee>> GetAll()
        {
            var Emps = _repository.GetEmployees().ToList();
            if (Emps.Count <= 0)
            {
                return NotFound();
            }
            return Ok(Emps);
        }


        [HttpGet("{id:int}")]
        public ActionResult<Employee> GetEmp(int id)
        {
            var emp = _repository.GetEmployee(id);
            if (emp is null)
            {
                return NotFound();
            }
            return Ok(emp);
        }


        [HttpGet("{id:int}/empImages")]
        public ActionResult<List<EmployeeFiles>> GetEmpImages(int id)
        {
            var empFiels = _repository.GetEmployeeImages(id);
            if (empFiels is null)
            {
                return NotFound();
            }
            return Ok(empFiels);
        }


        [HttpPost("search/{name}")]
        public ActionResult<List<Employee>> SearchEmps(string name)
        {
            var emps = _repository.SearchEmployees(name);
            if (emps is null)
            {
                return NotFound();
            }
            return Ok(emps);
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmp(int id)
        {
            var deleted = _repository.DeleteEmployee(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpPost("EmployeeImage")]
        public ActionResult<ImageDto> OnPostUploadAsync([FromForm] ImageDto image)
        {
            var success = _repository.UploadEmpImage(image);
            if (success)
            {
                return Ok("File Uploaded");
            }
            return BadRequest("Error Happend while uploading files or database!");
        }

        [HttpPost()]
        public ActionResult<EmployeeToAdd> AddEmp(EmployeeToAdd employee)
        {
            var success = _repository.AddEmployee(employee);
            if (success)
            {
                return Created("", null);
            }
            return BadRequest("error Happend");
        }

        [HttpPut("{id:int}")]
        public IActionResult EditEmp(int id, EmployeeToAdd employee)
        {
            var success = _repository.EditEmployee(id, employee);
            if (success)
            {
                return Ok();
            }
            return BadRequest("error Happend while editing");
        }

    }
}

