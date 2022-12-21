namespace DynamicFileReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DynamicFileHelper dynamicFileHelper = new DynamicFileHelper();
            var employees = dynamicFileHelper.ParseFile("EmployeeList.txt");
            foreach (var employee in employees)
            {
                //FirstName, LastName, City, State
                Console.WriteLine($"FirstName: {employee?.FirstName}, LastName: {employee?.LastName}," +
                    $"City: {employee?.City}, State: {employee?.State}");
            }
        }
    }
}