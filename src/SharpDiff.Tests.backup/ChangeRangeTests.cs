using NUnit.Framework;
using SharpDiff.Parsers.GitDiff;

namespace SharpDiff.Tests
{
    [TestFixture]
    public class ChangeRangeTests : AbstractParserTestFixture
    {
        [Test]
        public void OriginalChangeRangeParsed()
        {
            var result = Parse<ChangeRange>("-1,30", x => x.ChangeRange);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StartLine, Is.EqualTo(1));
            Assert.That(result.LinesAffected, Is.EqualTo(30));
        }

        [Test]
        public void NewChangeRangeParsed()
        {
            var result = Parse<ChangeRange>("+1,3", x => x.ChangeRange);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StartLine, Is.EqualTo(1));
            Assert.That(result.LinesAffected, Is.EqualTo(3));
        }

        [Test]
        public void AdditionsToEmptyFile()
        {
            var result = Parse<ChangeRange>("+1", x => x.ChangeRange);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StartLine, Is.EqualTo(1));
            Assert.That(result.LinesAffected, Is.EqualTo(1));
        }
    }
}