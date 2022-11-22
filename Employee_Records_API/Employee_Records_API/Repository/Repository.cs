using Employee_Records_API.DTO;
using Employee_Records_API.Helper;
using Employee_Records_API.Interfaces;
using Employee_Records_API.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Linq;

namespace Stored
{
    public class Repository : IRepository, IDisposable
    {
        private readonly SqlConnection conn = new SqlConnection(ConnectionString.CName);
        private bool disposedValue;
        private readonly IWebHostEnvironment _environment;
        public Repository(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IEnumerable<Employee> GetEmployees()
        {

            List<Employee> emps = new List<Employee>();
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetEmps", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        emps.Add(new Employee
                        {
                            Id = int.Parse(rdr["Id"].ToString()),
                            Name = rdr["Name"].ToString(),
                            Adress = rdr["Adress"].ToString(),
                            DateOfBirth = DateTime.Parse(rdr["DateOfBirth"].ToString()),
                            Department = rdr["Department"].ToString(),
                        });
                    }
                }
                conn.Close();
                Dispose();
            }

            return emps;
        }
        public Employee GetEmployee(int id)
        {
            Employee emp = new Employee();
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetEmp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@empId", id));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    var hasValues = rdr.HasRows;
                    if (!hasValues)
                    {
                        conn.Close();
                        return null;
                    }
                    rdr.Read();
                    emp.Id = int.Parse(rdr["Id"].ToString());
                    emp.Name = rdr["Name"].ToString();
                    emp.Adress = rdr["Adress"].ToString();
                    emp.DateOfBirth = DateTime.Parse(rdr["DateOfBirth"].ToString());
                    emp.Department = rdr["Department"].ToString();
                }
            }
            //emp.DateOfBirth = rdr["DateOfBirth"] is not null ? DateTime.Parse(rdr["DateOfBirth"].ToString()) : null;
            conn.Close();
            Dispose();
            return emp;
        }
        public IEnumerable<EmployeeFiles> GetEmployeeImages(int id)
        {
            List<EmployeeFiles> empFiles = new List<EmployeeFiles>();
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetEmpFiles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@empId", id));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    var hasValues = rdr.HasRows;
                    if (!hasValues)
                    {
                        conn.Close();
                        return null;
                    }
                    while (rdr.Read())
                    {
                        empFiles.Add(new EmployeeFiles
                        {
                            FileName = rdr["FileName"].ToString(),
                            FilePath = rdr["FilePath"].ToString(),
                        });
                    }

                }
            }
            conn.Close();
            Dispose();
            return empFiles;
        }
        public IEnumerable<Employee> SearchEmployees(string empName)
        {
            List<Employee> emps = new List<Employee>();
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SearchEmp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@empName", empName));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    var hasValues = rdr.HasRows;
                    if (!hasValues)
                    {
                        conn.Close();
                        return null;
                    }
                    while (rdr.Read())
                    {
                        emps.Add(new Employee
                        {
                            Id = int.Parse(rdr["Id"].ToString()),
                            Name = rdr["Name"].ToString(),
                            Adress = rdr["Adress"].ToString(),
                            DateOfBirth = DateTime.Parse(rdr["DateOfBirth"].ToString()),
                            Department = rdr["Department"].ToString(),
                        });
                    }

                }
            }
            conn.Close();
            Dispose();
            return emps;
        }
        public bool DeleteEmployee(int id)
        {
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DeleteEmp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@empId", id));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    var RecordsAffected = rdr.RecordsAffected;
                    if (RecordsAffected is 0)
                    {
                        conn.Close();
                        return false;
                    }
                    rdr.Read();
                }
            }
            conn.Close();
            Dispose();
            return true;
        }

        public bool AddEmployee(EmployeeToAdd employee)
        {
            var departmentExisit = DepartmentExist(employee.DepartmentId);
            if (!departmentExisit)
            {
                return false;
            }
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("AddEmp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@empName", employee.Name));
                cmd.Parameters.Add(new SqlParameter("@empDateOfBirth", employee.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@empAdress", employee.Adress));
                cmd.Parameters.Add(new SqlParameter("@DeptId", employee.DepartmentId));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    var RecordsAffected = rdr.RecordsAffected;
                    if (RecordsAffected is 0)
                    {
                        conn.Close();
                        return false;
                    }
                }
            }
            conn.Close();
            return true;
        }
        public bool EditEmployee(int id, EmployeeToAdd employee)
        {
            var employeExist = EmployeeExist(id);
            if (!employeExist)
            {
                return false;
            }
            var departmentExisit = DepartmentExist(employee.DepartmentId);
            if (!departmentExisit)
            {
                return false;
            }
            //SqlConnection conn2 = new SqlConnection(ConnectionString.CName);
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("EditEmp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("empId", id));
                cmd.Parameters.Add(new SqlParameter("@empName", employee.Name));
                cmd.Parameters.Add(new SqlParameter("@empDateOfBirth", employee.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@empAdress", employee.Adress));
                cmd.Parameters.Add(new SqlParameter("@DeptId", employee.DepartmentId));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    var RecordsAffected = rdr.RecordsAffected;
                    if (RecordsAffected is 0)
                    {
                        conn.Close();
                        return false;
                    }
                }
            }
            conn.Close();
            return true;
        }
        public bool UploadEmpImage(ImageDto image)
        {
            string root = _environment.WebRootPath;
            string uploadPath = Path.Combine(root, "Upload");
            try
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                if (image.Photo.Length > 0)
                {
                    var oldPath = Path.Combine(uploadPath, image.Photo.FileName);
                    var FileExtention = Path.GetExtension(oldPath).ToString();
                    var fileName = Guid.NewGuid().ToString() + FileExtention;
                    var path = Path.Combine(uploadPath, fileName);
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        image.Photo.CopyTo(stream);
                        stream.Close();
                    }
                    //
                    using (conn)
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("AddEmpFile", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@fileName", fileName));
                        cmd.Parameters.Add(new SqlParameter("@filepath", path));
                        cmd.Parameters.Add(new SqlParameter("@EmployeeId", image.EmployeeId));
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            var RecordsAffected = rdr.RecordsAffected;
                            if (RecordsAffected is 0)
                            {
                                conn.Close();
                                return false;
                            }
                            rdr.Read();
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
            return true;
        }


        public IEnumerable<Department> GetDepartments()
        {
            List<Department> depts = new List<Department>();
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetDepts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        depts.Add(new Department
                        {
                            Id = rdr.GetInt32("id"),
                            Name = rdr["Name"].ToString()
                        });
                    }
                }
                conn.Close();
                Dispose();
            }

            return depts;
        }
        public bool AddDepartment(string name)
        {
            Department dept = new Department();
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("AddDepartment", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@deptName", name));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    var RecordsAffected = rdr.RecordsAffected;
                    if (RecordsAffected is 0)
                    {
                        conn.Close();
                        return false;
                    }
                    rdr.Read();
                }
            }
            conn.Close();
            Dispose();
            return true;
        }
        public string GetDepartment(int id)
        {
            var departmentExisit = DepartmentExist(id);
            if (!departmentExisit)
            {
                return null;
            }
            string departmentName;
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetDept", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@deptId", id));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    var hasValues = rdr.HasRows;
                    if (!hasValues)
                    {
                        conn.Close();
                        return null;
                    }
                    rdr.Read();
                    departmentName = rdr["Name"].ToString();
                }
            }
            conn.Close();
            Dispose();
            return departmentName;
            ;
        }
        public int? GetDepartment(string name)
        {
            int departmentId;
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetDeptByName", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@deptName", name));
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    var hasValues = rdr.HasRows;
                    if (!hasValues)
                    {
                        conn.Close();
                        return null ;
                    }
                    rdr.Read();
                    departmentId = int.Parse(rdr["Id"].ToString());
                }
            }
            conn.Close();
            Dispose();
            return departmentId;
        }
        /// //////////////////
        private bool DepartmentExist(int deptId)
        {
            SqlConnection deptConn = new SqlConnection(ConnectionString.CName);

            using (deptConn)
            {
                deptConn.Open();
                SqlCommand cmd = new SqlCommand("GetDepts", deptConn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (rdr.GetInt32("id") == deptId)
                        {
                            deptConn.Close();
                            return true;
                        }
                    }
                    deptConn.Close();
                }
            }
            deptConn.Close();
            return false;
        }
        private bool EmployeeExist(int empId)
        {
            SqlConnection empConn = new SqlConnection(ConnectionString.CName);

            using (empConn)
            {
                empConn.Open();
                SqlCommand cmd = new SqlCommand("GetEmps", empConn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (rdr.GetInt32("Id") == empId)
                        {
                            empConn.Close();
                            return true;
                        }
                    }
                    empConn.Close();
                }
            }
            empConn.Close();
            return false;
        }

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Repository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
