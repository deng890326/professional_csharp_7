namespace OwnedEntities
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new PeopleContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            await AddSampleDataAsync(context);
        }

        private static async Task AddSampleDataAsync(PeopleContext context)
        {
            const string TAG = nameof(AddSampleDataAsync);
            Person person = new Person()
            {
                Name = "Deng Yingwei",
                CompanyAddress = new Address()
                {
                    LineOne = "company line one",
                    LineTwo = "company line two",
                    Location = new Location()
                    {
                        Country = "China",
                        City = "Shenzhen"
                    }
                },
                PrivateAddress = new Address()
                {
                    LineOne = "private line one",
                    LineTwo = "private line two",
                    Location = new Location()
                    {
                        Country = "USA",
                        City = "New York"
                    }
                }
            };
            context.People.Add(person);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{TAG}: {records} record saved.");
        }
    }
}