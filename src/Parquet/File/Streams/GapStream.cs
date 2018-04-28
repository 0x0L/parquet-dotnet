﻿using System.IO;

namespace Parquet.File.Streams
{
    class GapStream : Stream
    {
        readonly Stream _parent;
        readonly long? _knownLength;
        long _position;

        public GapStream(Stream parent, long? knownLength = null)
        {
            _parent = parent;
            _knownLength = knownLength;
        }

        public override bool CanRead => _parent.CanRead;

        public override bool CanSeek => _parent.CanSeek;

        public override bool CanWrite => _parent.CanWrite;

        public override long Length => _knownLength ?? _parent.Length;

        public override long Position
        {
            get => _position;
            set
            {
                _parent.Position = value;
                _position = value;
            }
        }

        public override void Flush()
        {
            _parent.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int read = _parent.Read(buffer, offset, count);

            _position += read;

            return read;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long pos = _parent.Seek(offset, origin);
            _position = pos;
            return pos;
        }

        public override void SetLength(long value)
        {
            _parent.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _parent.Write(buffer, offset, count);
            _position += count;
        }
    }
}
