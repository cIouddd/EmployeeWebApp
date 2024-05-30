using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Respository.Interfaces
{
   internal interface IEmployeeRepository
   {
      public interface IUserRepository
      {
         Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
         // Other CRUD operations can be added here
      }
   }
}
