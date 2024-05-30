using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Respository.Interfaces;
using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.Respository
{
   internal class EmployeeService : IEmployeeService
   {
      private readonly ApplicationDbContext _context;

      public EmployeeService(ApplicationDbContext context)
      {
         _context = context;
      }

      public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
      {
         try
         {
            var employees = await _context.Employee.ToListAsync();

            List<EmployeeDto> employeeResult = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
               if (!employee.IsDeleted)
               {
                  employeeResult.Add(new EmployeeDto
                  {
                     Id = employee.Id,
                     FullName = employee.FullName,
                     Birthdate = employee.Birthdate.ToString("yyyy-MM-dd"),
                     Tin = employee.Tin,
                     EmployeeTypeId = employee.EmployeeTypeId,
                  });
               }
            }

            return employeeResult;
         }
         catch (Exception ex)
         {
            // Log the exception
            throw new Exception("Database error: " + ex.Message);
         }
      }

      public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
      {
         try
         {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
               return null;
            }

            return new EmployeeDto
            {
               Id = employee.Id,
               FullName = employee.FullName,
               Birthdate = employee.Birthdate.ToString("yyyy-MM-dd"),
               Tin = employee.Tin,
               EmployeeTypeId = employee.EmployeeTypeId,
            };
         }
         catch (Exception ex)
         {
            // Log the exception
            throw new Exception("Database error: " + ex.Message);
         }
      }

      public async Task<Employee> CreateEmployeeAsync(CreateEmployeeDto employee)
      {
         try
         {
            Employee newEmployee = new Employee
            {
               FullName = employee.FullName,
               Birthdate = employee.Birthdate,
               Tin = employee.Tin,
               EmployeeTypeId = employee.EmployeeTypeId,
            };

            _context.Employee.Add(newEmployee);

            await _context.SaveChangesAsync();

            return newEmployee;
         }
         catch (Exception ex)
         {
            // Log the exception
            throw new Exception("Database error: " + ex.Message);
         }
      }

      public async Task<bool> SoftDeleteEmployeeAsync(int id)
      {
         try
         {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
               return false;
            }

            employee.IsDeleted = true;

            _context.Employee.Update(employee);

            await _context.SaveChangesAsync();

            return true;
         }
         catch (Exception ex)
         {
            // Log the exception
            throw new Exception("Database error: " + ex.Message);
         }
      }

      public async Task<bool> UpdateEmployeeAsync(int id, EditEmployeeDto employeeDto)
      {
         try
         {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null || employee.IsDeleted)
            {
               return false;
            }

            employee.Id = employeeDto.Id;
            employee.FullName = employeeDto.FullName;
            employee.Birthdate = employeeDto.Birthdate;
            employee.Tin = employeeDto.Tin;
            employee.EmployeeTypeId = employeeDto.EmployeeTypeId;

            _context.Employee.Update(employee);

            await _context.SaveChangesAsync();

            return true;
         }
         catch (Exception ex)
         {
            // Log the exception
            throw new Exception("Database error: " + ex.Message);
         }
      }

      public Task<decimal> CalculateSalaryAsync(int id, decimal absentDays, decimal workedDays)
      {
         var employee = _context.Employee.Find(id);

         if (employee == null)
            throw new ArgumentException("Employee not found");

         switch (employee.EmployeeTypeId)
         {
            case (int)EmployeeType.Regular:
               RegularEmployee regularEmployee = new RegularEmployee();
               regularEmployee.absences = absentDays;
               return Task.FromResult(regularEmployee.ComputeSalary());

            case (int)EmployeeType.Contractual:
               ContractualEmployee contractualEmployee = new ContractualEmployee();
               contractualEmployee.daysWorked = workedDays;
               return Task.FromResult(contractualEmployee.ComputeSalary());

            default:
               throw new ArgumentException("Invalid employee type");
         }
      }
   }
}

