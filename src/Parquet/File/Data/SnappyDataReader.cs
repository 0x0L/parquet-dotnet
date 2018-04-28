using System.IO;
using Snappy.Sharp;

namespace Parquet.File.Data
{
    class SnappyDataReader : IDataReader
    {
        readonly SnappyDecompressor _snappyDecompressor = new SnappyDecompressor();

        public byte[] Read(Stream source, int count)
        {
            byte[] buffer = new byte[count];
            source.Read(buffer, 0, count);
            byte[] uncompressedBytes = _snappyDecompressor.Decompress(buffer, 0, count);
            return uncompressedBytes;
        }
    }
}
