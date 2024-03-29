﻿using System;
using System.Collections.Generic;
using System.IO;
using Parquet.Data;

namespace Parquet.File
{
    class ParquetRowGroupReader : IDisposable
    {
        readonly Thrift.RowGroup _rowGroup;
        readonly ThriftFooter _footer;
        readonly Stream _stream;
        readonly ThriftStream _thriftStream;
        readonly ParquetOptions _parquetOptions;
        readonly Dictionary<string, Thrift.ColumnChunk> _pathToChunk = new Dictionary<string, Thrift.ColumnChunk>();

        internal ParquetRowGroupReader(
           Thrift.RowGroup rowGroup,
           ThriftFooter footer,
           Stream stream, ThriftStream thriftStream,
           ParquetOptions parquetOptions)
        {
            _rowGroup = rowGroup ?? throw new ArgumentNullException(nameof(rowGroup));
            _footer = footer ?? throw new ArgumentNullException(nameof(footer));
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _thriftStream = thriftStream ?? throw new ArgumentNullException(nameof(thriftStream));
            _parquetOptions = parquetOptions ?? throw new ArgumentNullException(nameof(parquetOptions));

            //cache chunks
            foreach (Thrift.ColumnChunk thriftChunk in _rowGroup.Columns)
            {
                string path = thriftChunk.GetPath();
                _pathToChunk[path] = thriftChunk;
            }
        }

        /// <summary>
        /// Gets the number of rows in this row group
        /// </summary>
        public long RowCount => _rowGroup.Num_rows;

        /// <summary>
        /// Reads a column from this row group.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public DataColumn ReadColumn(DataField field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            if (!_pathToChunk.TryGetValue(field.Path, out Thrift.ColumnChunk columnChunk))
            {
                throw new NotImplementedException();
            }

            var columnReader = new DataColumnReader(field, _stream, columnChunk, _footer, _parquetOptions);

            return columnReader.Read();
        }

        public void Dispose()
        {
            //don't need to dispose anything here, but for clarity we implement IDisposable and client must use it as we may add something
            //important in it later
        }

    }
}
