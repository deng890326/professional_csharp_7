namespace UnitTestingSamples
{
    [TestClass]
    public class DeepThoughtTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int expected = 42;
            DeepThought deepThought = new DeepThought();

            int actual = deepThought.TheAnswerOfTheUltimateQuestionOfLifeTheUniverseAndEverything();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod2()
        {
            var sample = new StringSample(null);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string expected = "b not found in a";
            var sample = new StringSample("");

            string actual = sample.GetStringDemo("a", "b");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string expected = "removed bc from abcd: ad";
            var sample = new StringSample("");

            string actual = sample.GetStringDemo("abcd", "bc");

            Assert.AreEqual(expected, actual);
        }
    }
}