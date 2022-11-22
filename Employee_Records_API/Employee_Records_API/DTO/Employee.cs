using System.ComponentModel.DataAnnotations;

namespace Employee_Records_API.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Adress { get; set; }
        public string Department { get; set; }
    }
}
