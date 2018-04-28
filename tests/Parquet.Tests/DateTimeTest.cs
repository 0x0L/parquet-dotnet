using Xunit;

namespace Parquet.Tests
{
    public class DateTimeTest : TestBase
    {
        [Fact]
        public void Numpy_dates()
        {
            var ds = ParquetReader.Read(OpenTestFile("numpy_dates.parquet"));
            var ds1 = ds.WriteRead();
        }
    }
}
