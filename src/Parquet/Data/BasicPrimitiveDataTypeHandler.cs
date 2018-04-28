using System.Collections;
using System.Collections.Generic;

namespace Parquet.Data
{
    abstract class BasicPrimitiveDataTypeHandler<TSystemType> : BasicDataTypeHandler<TSystemType>
       where TSystemType : struct
    {
        protected BasicPrimitiveDataTypeHandler(DataType dataType, Thrift.Type thriftType, Thrift.ConvertedType? convertedType = null)
            : base(dataType, thriftType, convertedType)
        {
        }

        public override IList CreateEmptyList(bool isNullable, bool isArray, int capacity)
        {
            if (isArray)
            {
                return isNullable
                    ? new List<List<TSystemType?>>(capacity)
                    : (IList)(new List<List<TSystemType>>(capacity));
            }
            return isNullable
                ? new List<TSystemType?>(capacity)
                : (IList)(new List<TSystemType>(capacity));
        }

        public override IList InsertDefinitions(
            IList values, int maxDefinitionLevel, List<int> definitions,
            out List<bool> hasValueFlags)
        {
            hasValueFlags = null;
            if (definitions == null || !values.IsNullable()) return values;
            hasValueFlags = new List<bool>();

            var z = new TSystemType?[definitions.Count];

            var it = values.GetEnumerator();
            it.MoveNext();

            for (int i = 0; i < definitions.Count; i++)
            {
                int def = definitions[i];
                bool hasValue = true;

                if (def == 0)
                {
                    z[i] = null;
                }
                else if (def != maxDefinitionLevel)
                {
                    z[i] = null;
                    hasValue = false;
                }
                else
                {
                    z[i] = it.Current as TSystemType?;
                    it.MoveNext();
                }

                hasValueFlags.Add(hasValue);
            }

            return z;
        }
    }
}
