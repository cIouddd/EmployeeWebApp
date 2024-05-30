namespace Sprout.Exam.WebApp.Data
{
   public class ContractualEmployee : Employee
   {
      public decimal daysWorked { get; set; } // Number of days worked

      public override decimal ComputeSalary()
      {
         const int dailyRate = 500;

         return dailyRate * daysWorked; // No tax deduction
      }
   }
}
