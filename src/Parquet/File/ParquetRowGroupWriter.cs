using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Parquet.Data;

namespace Parquet.File
{
    class ParquetRowGroupWriter : IDisposable
    {
        readonly Schema _schema;
        readonly Stream _stream;
        readonly ThriftStream _thriftStream;
        readonly ThriftFooter _footer;
        readonly CompressionMethod _compressionMethod;
        readonly ParquetOptions _formatOptions;
        readonly int _rowCount;
        readonly Thrift.RowGroup _thriftRowGroup;
        readonly long _rgStartPos;
        readonly List<Thrift.SchemaElement> _thschema;
        int _colIdx;

        internal ParquetRowGroupWriter(Schema schema,
           Stream stream,
           ThriftStream thriftStream,
           ThriftFooter footer,
           CompressionMethod compressionMethod,
           ParquetOptions formatOptions,
           int rowCount)
        {
            _schema = schema ?? throw new ArgumentNullException(nameof(schema));
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _thriftStream = thriftStream ?? throw new ArgumentNullException(nameof(thriftStream));
            _footer = footer ?? throw new ArgumentNullException(nameof(footer));
            _compressionMethod = compressionMethod;
            _formatOptions = formatOptions;
            _rowCount = rowCount;

            _thriftRowGroup = _footer.AddRowGroup();
            _thriftRowGroup.Num_rows = _rowCount;
            _rgStartPos = _stream.Position;
            _thriftRowGroup.Columns = new List<Thrift.ColumnChunk>();
            _thschema = _footer.GetWriteableSchema().ToList();
        }

        public void Write(DataColumn column)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));

            Thrift.SchemaElement tse = _thschema[_colIdx++];
            IDataTypeHandler dataTypeHandler = DataTypeFactory.Match(tse, _formatOptions);
            //todo: check if the column is in the right order

            List<string> path = _footer.GetPath(tse);

            var writer = new DataColumnWriter(_stream, _thriftStream, _footer, tse, _compressionMethod, _rowCount);

            Thrift.ColumnChunk chunk = writer.Write(path, column, dataTypeHandler);
            _thriftRowGroup.Columns.Add(chunk);
        }

        public void Dispose()
        {
            //todo: check if all columns are present

            //row group's size is a sum of _uncompressed_ sizes of all columns in it, including the headers
            //luckily ColumnChunk already contains sizes of page+header in it's meta
            _thriftRowGroup.Total_byte_size = _thriftRowGroup.Columns.Sum(c => c.Meta_data.Total_compressed_size);
        }
    }
}
