using Employee_Records_API.DTO;
using Employee_Records_API.Model;

namespace Employee_Records_API.Interfaces
{
    public interface IRepository
    {
        public IEnumerable<Employee> GetEmployees();
        public Employee GetEmployee(int id);
        public IEnumerable<EmployeeFiles> GetEmployeeImages(int id);
        public IEnumerable<Employee> SearchEmployees(string empName);
        public bool DeleteEmployee(int id);
        public bool AddEmployee(EmployeeToAdd employee);
        public bool UploadEmpImage(ImageDto image);

        public bool EditEmployee(int id, EmployeeToAdd employee);
        public string GetDepartment(int id);
        public int? GetDepartment(string name);


        public IEnumerable<Department> GetDepartments();
        public bool AddDepartment(string name);

    }
}
