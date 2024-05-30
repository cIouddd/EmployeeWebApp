using System;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Data
{
   public class Employee
   {
      public int Id { get; set; }
      public string FullName { get; set; }
      public DateTime Birthdate { get; set; }
      public string Tin { get; set; }
      public int EmployeeTypeId { get; set; }
      public bool IsDeleted { get; set; }

      public virtual decimal ComputeSalary() 
      {
         throw new NotImplementedException();
      }
   }
}
