using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess;
using Sprout.Exam.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Respository
{
   internal class EmployeeRepository : IEmployeeRepository
   {
      private readonly ApplicationDbContext _context;

      public EmployeeRepository(ApplicationDbContext context)
      {
         _context = context;
      }

      //public async Task<EmployeeDto> GetEmployees()
      //{
      //   return await _context.E.FindAsync(id);
      //}
   }
}

