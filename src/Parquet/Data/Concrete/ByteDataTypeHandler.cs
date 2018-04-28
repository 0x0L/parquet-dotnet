﻿using System.IO;

namespace Parquet.Data.Concrete
{
    class ByteDataTypeHandler : BasicPrimitiveDataTypeHandler<byte>
    {
        public ByteDataTypeHandler() : base(DataType.Byte, Thrift.Type.INT32, Thrift.ConvertedType.UINT_8)
        {

        }

        protected override byte ReadOne(BinaryReader reader)
        {
            return reader.ReadByte();
        }

        protected override void WriteOne(BinaryWriter writer, byte value)
        {
            writer.Write(value);
        }
    }
}