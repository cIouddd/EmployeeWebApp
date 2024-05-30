namespace Sprout.Exam.Business.DataTransferObjects
{
   public class EmployeeDto
    {
      public int Id { get; set; }
      public string FullName { get; set; }
      public string Birthdate { get; set; }
      public string Tin { get; set; }
      public int EmployeeTypeId { get; set; }
      public decimal WorkedDays { get; set; }
      public decimal AbsentDays { get; set; }
   }
}
