using System.Collections.Generic;
using Parquet.Data;
using Parquet.Serialization.Values;

namespace Parquet.Serialization
{
    /// <summary>
    /// Extracts data from CLR structures
    /// </summary>
    class ColumnExtractor
    {
        /// <summary>
        /// Extracts data columns from a collection of CLR class instances
        /// </summary>
        /// <typeparam name="TClass">Class type</typeparam>
        /// <param name="classInstances">Collection of class instances</param>
        /// <param name="schema">Schema to operate on</param>
        public IReadOnlyCollection<DataColumn> ExtractColumns<TClass>(IEnumerable<TClass> classInstances, Schema schema)
        {
            List<DataField> dataFields = schema.GetDataFields();

            IColumnClrMapper valuesExtractor = new SlowReflectionColumnClrMapper(typeof(TClass));
            IReadOnlyCollection<DataColumn> result = valuesExtractor.ExtractDataColumns(dataFields, classInstances);

            return result;
        }
    }
}
