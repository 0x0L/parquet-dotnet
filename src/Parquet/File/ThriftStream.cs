using System.IO;
using System.Threading;
using Thrift.Protocols;
using Thrift.Transports;
using Thrift.Transports.Client;

namespace Parquet.File
{
    /// <summary>
    /// Utility methods to work with Thrift data in a stream
    /// </summary>
    class ThriftStream
    {
        readonly Stream _s;
        readonly TProtocol _protocol;

        public ThriftStream(Stream s)
        {
            _s = s;
            TClientTransport transport = new TStreamClientTransport(s, s);
            _protocol = new TCompactProtocol(transport);
        }

        /// <summary>
        /// Reads typed structure from incoming stream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Read<T>() where T : TBase, new()
        {
            var res = new T();
            res.ReadAsync(_protocol, CancellationToken.None).Wait();
            return res;
        }

        /// <summary>
        /// Writes types structure to the destination stream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="rewind">When true, rewinds to the original position before writing</param>
        /// <returns>Actual size of the object written</returns>
        public int Write<T>(T obj, bool rewind = false) where T : TBase, new()
        {
            _s.Flush();
            long startPos = _s.Position;
            obj.WriteAsync(_protocol, CancellationToken.None).Wait();
            _s.Flush();
            long size = _s.Position - startPos;
            if (rewind) _s.Seek(startPos, SeekOrigin.Begin);
            return (int)size;
        }
    }
}
