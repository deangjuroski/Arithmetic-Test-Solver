using System.Data;

namespace StudentMathTestSystem.Utilities
{
    public static class MathProcessor
    {
        public static bool ValidateTask(string expression, int expectedResult)
        {
            var dataTable = new DataTable();
            var calculatedResult = Convert.ToDouble(dataTable.Compute(expression, string.Empty));

            return Math.Abs(calculatedResult - expectedResult) < 0.0001; 
        }
    }
}
