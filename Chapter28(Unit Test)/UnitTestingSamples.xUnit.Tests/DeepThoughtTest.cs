using Xunit;

namespace UnitTestingSamples.xUnit.Tests
{
    public class DeepThoughtTest
    {
        [Fact]
        public void Test1()
        {
            int expected = 42;
            DeepThought deep = new DeepThought();

            int actual = deep.TheAnswerOfTheUltimateQuestionOfLifeTheUniverseAndEverything();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test2()
        {
            var sample = new StringSample("");
            Assert.Throws<ArgumentNullException>("first", () => sample.GetStringDemo(null, null));
            Assert.Throws<ArgumentNullException>("first", () => sample.GetStringDemo(null, ""));
            Assert.Throws<ArgumentNullException>("second", () => sample.GetStringDemo("a", null));
            Assert.Throws<ArgumentException>("first", () => sample.GetStringDemo("", null));
            Assert.Throws<ArgumentOutOfRangeException>("second", () => sample.GetStringDemo("a", "ab"));
            Assert.Throws<ArgumentNullException>("init", () => new StringSample(null));
        }

        [Theory]
        [InlineData("init", "abc", "def", "def not found in abc")]
        [InlineData("init", "abc", "", "removed  from abc: abc")]
        [InlineData("init", "longer string", "nger", "removed nger from longer string: lo string")]
        [InlineData("init", "longer string", "string", "INIT")]
        public void GetStringDemoTest(string init, string first, string second, string expected)
        {
            var sample = new StringSample(init);
            string actual = sample.GetStringDemo(first, second);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(StringSampleData))]
        public void GetStringDemoMemberDataTest(StringSample sample, string first, string second, string expected)
        {
            string actual = sample.GetStringDemo(first, second);
            Assert.Equal(expected, actual);
        }

        public static readonly IEnumerable<object[]> StringSampleData = new object[][]
        {
            new object[] { new StringSample("init"), "abc", "def", "def not found in abc" },
            new object[] { new StringSample("init"), "abc", "", "removed  from abc: abc" },
            new object[] { new StringSample("init"), "longer string", "nger", "removed nger from longer string: lo string" },
            new object[] { new StringSample("init"), "longer string", "string", "INIT" },
        };
    }
}