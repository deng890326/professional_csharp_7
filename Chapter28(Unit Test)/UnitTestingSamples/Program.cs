namespace UnitTestingSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            DeepThought deepThought = new DeepThought();
            Console.WriteLine($"{nameof(deepThought.TheAnswerOfTheUltimateQuestionOfLifeTheUniverseAndEverything)}: " +
                $"{deepThought.TheAnswerOfTheUltimateQuestionOfLifeTheUniverseAndEverything()}");
        }
    }
}