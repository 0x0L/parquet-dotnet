using System.IO;
using System.Numerics;

namespace Parquet.Data.Concrete
{
    class Int96DataTypeHandler : BasicPrimitiveDataTypeHandler<BigInteger>
    {
        public Int96DataTypeHandler() : base(DataType.Int96, Thrift.Type.INT96)
        {
        }

        public override bool IsMatch(Thrift.SchemaElement tse, ParquetOptions formatOptions)
        {
            return tse.Type == Thrift.Type.INT96 && !formatOptions.TreatBigIntegersAsDates;
        }

        protected override BigInteger ReadOne(BinaryReader reader)
        {
            byte[] data = reader.ReadBytes(12);
            var big = new BigInteger(data);
            return big;
        }

        protected override void WriteOne(BinaryWriter writer, BigInteger value)
        {
            byte[] data = value.ToByteArray();
            writer.Write(data);
        }
    }
}
