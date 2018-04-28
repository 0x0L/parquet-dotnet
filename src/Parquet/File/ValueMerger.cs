using System;
using System.Collections;
using System.Collections.Generic;
using Parquet.Data;

namespace Parquet.File
{
    /// <summary>
    /// Responsible for merging values from different parts of column parts (repetition, definitions etc.)
    /// </summary>
    class ValueMerger
    {
        readonly int _maxDefinitionLevel;
        readonly int _maxRepetitionLevel;
        readonly Func<IList> _createEmptyListFunc;
        IList _values;
        IDataTypeHandler _dataTypeHandler;
        bool _isNullable;

        public ValueMerger(
           int maxDefinitionLevel,
           int maxRepetitionLevel,
           IList values,
           IDataTypeHandler dataTypeHandler,
           bool isNullable)
        {
            _maxDefinitionLevel = maxDefinitionLevel;
            _maxRepetitionLevel = maxRepetitionLevel;
            _dataTypeHandler = dataTypeHandler;
            _isNullable = isNullable;
            _createEmptyListFunc = () => _dataTypeHandler.CreateEmptyList(_isNullable, false, 0);
            _values = values ?? _dataTypeHandler.CreateEmptyList(_isNullable, false, 0);
        }

        /// <summary>
        /// Applies dictionary with indexes and definition levels directly over the column
        /// </summary>
        public IList Apply(IList dictionary, List<int> definitions, List<int> repetitions, List<int> indexes, int maxValues)
        {
            if (dictionary == null && definitions == null && indexes == null && repetitions == null) return _values;  //values are just values

            ApplyDictionary(dictionary, indexes, maxValues);

            List<bool> hasValueFlags;
            _values = _dataTypeHandler.InsertDefinitions(_values, _maxDefinitionLevel, definitions, out hasValueFlags);
            _values = RepetitionPack.FlatToHierarchy(_maxRepetitionLevel, _createEmptyListFunc, _values, repetitions, hasValueFlags);

            return _values;
        }

        void ApplyDictionary(IList dictionary, List<int> indexes, int maxValues)
        {
            //merge with dictionary if present
            if (dictionary == null) return;

            //when dictionary has no indexes
            if (indexes == null) return;

            indexes.TrimTail(maxValues);

            foreach (int index in indexes)
            {
                object value = dictionary[index];
                _values.Add(value);
            }
        }
    }
}
