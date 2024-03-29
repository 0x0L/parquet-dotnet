﻿using System;
using System.IO;
using System.IO.Compression;
using Snappy.Sharp;

namespace Parquet.File.Streams
{
    /// <summary>
    /// In-memory hacky implementation of Snappy streaming as Snappy.Sharp's implementation is a work in progress
    /// </summary>
    class SnappyInMemoryStream : Stream
    {
        readonly Stream _parent;
        readonly CompressionMode _compressionMode;
        readonly MemoryStream _ms;

        public SnappyInMemoryStream(Stream parent, CompressionMode compressionMode)
        {
            _parent = parent;
            _compressionMode = compressionMode;

            if (compressionMode == CompressionMode.Compress)
            {
                _ms = new MemoryStream();
            }
            else
            {
                _ms = DecompressFromStream(parent);
            }
        }

        public override bool CanRead => _compressionMode == CompressionMode.Decompress;

        public override bool CanSeek => false;

        public override bool CanWrite => _compressionMode == CompressionMode.Compress;

        public override long Length => throw new NotSupportedException();

        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override void Flush()
        {
            if (_compressionMode == CompressionMode.Compress)
            {
                //compress memory buffer and write to destination
                var snappyCompressor = new SnappyCompressor();
                int uncompressedLength = (int)_ms.Length;
                int compressedSize = snappyCompressor.MaxCompressedLength(uncompressedLength);
                byte[] compressed = new byte[compressedSize];
                int length = snappyCompressor.Compress(_ms.ToArray(), 0, uncompressedLength, compressed);
                _parent.Write(compressed, 0, length);
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _ms.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _ms.Write(buffer, offset, count);
        }

        MemoryStream DecompressFromStream(Stream source)
        {
            var snappyDecompressor = new SnappyDecompressor();

            byte[] buffer = new byte[source.Length];
            source.Read(buffer, 0, (int)source.Length);
            byte[] uncompressedBytes = snappyDecompressor.Decompress(buffer, 0, (int)source.Length);
            return new MemoryStream(uncompressedBytes);

        }
    }
}
