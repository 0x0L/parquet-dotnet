using System;
using System.Collections;
using System.IO;
using Parquet.File.Values.Primitives;

namespace Parquet.Data.Concrete
{
    class DateTimeDataTypeHandler : BasicPrimitiveDataTypeHandler<DateTime>
    {
        public DateTimeDataTypeHandler() : base(DataType.DateTimeOffset, Thrift.Type.BYTE_ARRAY)
        {

        }

        public override bool IsMatch(Thrift.SchemaElement tse, ParquetOptions formatOptions)
        {
            return 
                (tse.Type == Thrift.Type.INT96 && formatOptions.TreatBigIntegersAsDates) || //Impala

                (tse.Type == Thrift.Type.INT64 && tse.__isset.converted_type && tse.Converted_type == Thrift.ConvertedType.TIMESTAMP_MILLIS) ||
                (tse.Type == Thrift.Type.INT64 && tse.__isset.converted_type && tse.Converted_type == Thrift.ConvertedType.TIMESTAMP_MICROS) ||

                (tse.Type == Thrift.Type.INT32 && tse.__isset.converted_type && tse.Converted_type == Thrift.ConvertedType.DATE);
        }

        public override IList Read(Thrift.SchemaElement tse, BinaryReader reader, ParquetOptions formatOptions)
        {
            IList result = CreateEmptyList(tse.IsNullable(), false, 0);

            switch (tse.Type)
            {
                case Thrift.Type.INT32:
                    ReadAsInt32(reader, result);
                    break;

                case Thrift.Type.INT64:
                    ReadAsInt64(reader, result, tse.Converted_type);
                    break;

                case Thrift.Type.INT96:
                    ReadAsInt96(reader, result);
                    break;

                default:
                    throw new InvalidDataException($"data type '{tse.Type}' does not represent any date types");
            }

            return result;
        }

        void ReadAsInt32(BinaryReader reader, IList result)
        {
            while (reader.BaseStream.Position + 4 <= reader.BaseStream.Length)
            {
                int iv = reader.ReadInt32();
                result.Add(iv.FromUnixTime());
            }
        }

        void ReadAsInt64(BinaryReader reader, IList result, Thrift.ConvertedType type)
        {
            bool isMicro = (type != Thrift.ConvertedType.TIMESTAMP_MILLIS);
            while (reader.BaseStream.Position + 8 <= reader.BaseStream.Length)
            {
                long lv = reader.ReadInt64();
                var r = isMicro ? lv.FromTimeStampMicro() : lv.FromUnixTime();
                result.Add(r);
            }
        }

        void ReadAsInt96(BinaryReader reader, IList result)
        {
            while (reader.BaseStream.Position + 12 <= reader.BaseStream.Length)
            {
                var nano = new NanoTime(reader.ReadBytes(12), 0);
                DateTimeOffset dt = nano;
                result.Add(dt.UtcDateTime);
            }
        }
    }
}
