using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.Respository.Interfaces
{
   public interface IEmployeeService
   {
      Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
      Task<EmployeeDto> GetEmployeeByIdAsync(int id);
      Task<Employee> CreateEmployeeAsync(CreateEmployeeDto employee);
      Task<bool> SoftDeleteEmployeeAsync(int id);
      Task<bool> UpdateEmployeeAsync(int id, EditEmployeeDto employeeDto);
      Task<decimal> CalculateSalaryAsync(int id, decimal absentDays, decimal workedDays);

      // Other CRUD operations can be added here
   }
}
