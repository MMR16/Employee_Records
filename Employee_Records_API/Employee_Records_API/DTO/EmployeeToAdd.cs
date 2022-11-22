using System.ComponentModel.DataAnnotations;

namespace Employee_Records_API.DTO
{
    public class EmployeeToAdd
    {
        [Required, MaxLength(500, ErrorMessage = "Name Must Be Less 500 Letter")]
        public string Name { get; set; }


        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        [MaxLength(1000, ErrorMessage = "Adress Must Be Less 1000 Letter")]
        public string Adress { get; set; }
        public int DepartmentId { get; set; }
    }
}
