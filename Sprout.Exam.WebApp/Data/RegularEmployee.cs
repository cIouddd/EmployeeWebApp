namespace Sprout.Exam.WebApp.Data
{
   public class RegularEmployee : Employee
   {
      public decimal absences { get; set; } // Number of absence days

      public override decimal ComputeSalary()
      {
         const int workingDaysInMonth = 22;
         const decimal monthlySalary = 20000;

         decimal dailyRate = monthlySalary / workingDaysInMonth;

         decimal absenceDeduction = dailyRate * absences;

         decimal grossSalary = monthlySalary - absenceDeduction;

         decimal netSalary = grossSalary * 0.88M; // 12% tax deduction

         return netSalary;
      }
   }
}
