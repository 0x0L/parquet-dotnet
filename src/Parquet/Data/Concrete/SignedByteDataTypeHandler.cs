﻿using System.IO;

namespace Parquet.Data.Concrete
{
    class SignedByteDataTypeHandler : BasicPrimitiveDataTypeHandler<sbyte>
    {
        public SignedByteDataTypeHandler() : base(DataType.SignedByte, Thrift.Type.INT32, Thrift.ConvertedType.INT_8)
        {

        }

        protected override sbyte ReadOne(BinaryReader reader)
        {
            return reader.ReadSByte();
        }

        protected override void WriteOne(BinaryWriter writer, sbyte value)
        {
            writer.Write(value);
        }
    }
}
