using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Parquet.Data.Concrete
{
    class DoubleDataTypeHandler : BasicPrimitiveDataTypeHandler<double>
    {
        public DoubleDataTypeHandler() : base(DataType.Double, Thrift.Type.DOUBLE)
        {

        }

        protected override double ReadOne(BinaryReader reader)
        {
            return reader.ReadDouble();
        }

        protected override void WriteOne(BinaryWriter writer, double value)
        {
            writer.Write(value);
        }

        public override IList InsertDefinitions(
            IList values, int maxDefinitionLevel, List<int> definitions,
            out List<bool> hasValueFlags)
        {
            hasValueFlags = null;
            if (definitions == null) return values;
            hasValueFlags = new List<bool>();

            var z = new double[definitions.Count];

            var it = values.GetEnumerator();
            it.MoveNext();

            for (int i = 0; i < definitions.Count; i++)
            {
                int def = definitions[i];
                bool hasValue = true;

                if (def == 0)
                {
                    z[i] = double.NaN;
                }
                else if (def != maxDefinitionLevel)
                {
                    z[i] = double.NaN;
                    hasValue = false;
                }
                else
                {
                    z[i] = (double)(it.Current);
                    it.MoveNext();
                }

                hasValueFlags.Add(hasValue);
            }

            return z;
        }

        public override IList CreateEmptyList(bool isNullable, bool isArray, int capacity)
        {
            return isArray
                ? new List<List<double>>(capacity)
                : (IList)(new List<double>(capacity));
        }
    }
}
