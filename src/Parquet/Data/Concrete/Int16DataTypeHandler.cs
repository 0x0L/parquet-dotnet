﻿using System.IO;

namespace Parquet.Data.Concrete
{
    class Int16DataTypeHandler : BasicPrimitiveDataTypeHandler<short>
    {
        public Int16DataTypeHandler() : base(DataType.Short, Thrift.Type.INT32, Thrift.ConvertedType.INT_16)
        {

        }

        protected override short ReadOne(BinaryReader reader)
        {
            return reader.ReadInt16();
        }

        protected override void WriteOne(BinaryWriter writer, short value)
        {
            writer.Write(value);
        }
    }
}
