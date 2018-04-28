using Parquet.Data;
using System.IO;

namespace Parquet.Tests
{
    static class DataSetGenerator
    {
        public static DataSet WriteRead(this DataSet ds)
        {
            return WriteReadOpt(ds, null);
        }

        public static string WriteReadFirstRow(this DataSet ds)
        {
            DataSet ds1 = WriteRead(ds);
            return ds1[0].ToString();
        }

        public static DataSet Generate(int rowCount)
        {
            var ds = new DataSet(new DataField<int>("id"));
            for (int i = 0; i < rowCount; i++)
            {
                var row = new Row(i);
                ds.Add(row);
            }
            return ds;
        }

        public static DataSet WriteReadOpt(DataSet original, WriterOptions writerOptions = null)
        {
            var ms = new MemoryStream();

            ParquetWriter.Write(original, ms, CompressionMethod.None, null, writerOptions);
            ms.Flush();
            //System.IO.File.WriteAllBytes("c:\\tmp\\wr.parquet", ms.ToArray());

            ms.Position = 0;
            return ParquetReader.Read(ms);
        }
    }
}
