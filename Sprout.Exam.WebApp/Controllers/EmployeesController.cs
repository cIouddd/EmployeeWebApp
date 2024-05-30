using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Respository.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
   [Authorize]
   [Route("api/[controller]")]
   [ApiController]
   public class EmployeesController : ControllerBase
   {
      private readonly IEmployeeService _employeeService;

      public EmployeesController(IEmployeeService employeeService)
      {
         _employeeService = employeeService;
      }

      /// <summary>
      /// Refactor this method to go through proper layers and fetch from the DB.
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public async Task<IActionResult> Get()
      {
         try
         {
            var employees = await _employeeService.GetEmployeesAsync();

            return Ok(employees);
         }
         catch (Exception ex)
         {
            Trace.WriteLine(ex.ToString());
            return BadRequest(ex.ToString());
         }
      }

      /// <summary>
      /// Refactor this method to go through proper layers and fetch from the DB.
      /// </summary>
      /// <returns></returns>
      [HttpGet("{id}")]
      public async Task<IActionResult> GetById(int id)
      {
         try
         {
            var result = await _employeeService.GetEmployeeByIdAsync(id);

            return Ok(result);
         }
         catch (Exception ex)
         {
            Trace.WriteLine(ex.ToString());
            return BadRequest(ex.ToString());
         }
      }

      /// <summary>
      /// Refactor this method to go through proper layers and update changes to the DB.
      /// </summary>
      /// <returns></returns>
      [HttpPut("{id}")]
      public async Task<IActionResult> Put(int id, EditEmployeeDto input)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var result = await _employeeService.UpdateEmployeeAsync(id, input);

         if (!result)
            return NotFound();

         return Ok(result);
      }

      /// <summary>
      /// Refactor this method to go through proper layers and insert employees to the DB.
      /// </summary>
      /// <returns></returns>
      [HttpPost]
      public async Task<IActionResult> Post(CreateEmployeeDto input)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         try
         {
            var employee = await _employeeService.CreateEmployeeAsync(input);

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
         }
         catch (Exception ex)
         {
            Trace.WriteLine(ex.ToString());
            return BadRequest(ex.ToString());
         }
      }


      /// <summary>
      /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
      /// </summary>
      /// <returns></returns>
      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
         var result = await _employeeService.SoftDeleteEmployeeAsync(id);

         if (!result)
            return NotFound();

         return Ok(result);
      }



      /// <summary>
      /// Refactor this method to go through proper layers and use Factory pattern
      /// </summary>
      /// <param name="id"></param>
      /// <param name="absentDays"></param>
      /// <param name="workedDays"></param>
      /// <returns></returns>
      [HttpPost("{id}/{absentDays}/{workedDays}/calculate")]
      public async Task<IActionResult> Calculate(int id, decimal absentDays, decimal workedDays)
      {
         try
         {
            var salary = await _employeeService.CalculateSalaryAsync(id, absentDays, workedDays);
            return Ok(salary);
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }
      }

   }
}
