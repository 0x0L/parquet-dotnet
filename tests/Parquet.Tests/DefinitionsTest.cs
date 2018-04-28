using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parquet.Data;
using Parquet.File;
using Xunit;

namespace Parquet.Tests
{
    public class DefinitionsTest
    {
        [Fact]
        public void Level4_definitions_packed_when_none_are_null()
        {
            var values = new List<int?> { 1, 2, 1, 2 };
            var dh = DataTypeFactory.Match(typeof(int));
            var v = dh.InsertDefinitions(values, 4, new List<int> { 4, 4, 4, 4 }, out var _);

            Assert.Equal(4, v.Count);
            Assert.Equal(Nullable<int>(1, 2, 1, 2), v);
        }

        [Fact]
        public void First_and_second_is_null_packed()
        {
            var values = new List<int?> { 1, 2 };
            var dh = DataTypeFactory.Match(typeof(int));
            var v = dh.InsertDefinitions(values, 1, new List<int> { 0, 0, 1, 1 }, out var _);

            Assert.Equal(4, v.Count);
            Assert.Equal(Nullable<int>(null, null, 1, 2), v);
        }

        [Fact]
        public void First_and_second_is_null_unpacked()
        {
            var list = new List<int?> { null, null, 1, 2 };
            IList definitions = DefinitionPack.RemoveNulls(list, 1);

            Assert.Equal(new int?[] { 1, 2 }, list);
            Assert.Equal(new int[] { 0, 0, 1, 1 }, definitions);
        }

        [Fact]
        public void First_and_lastis_null_packed()
        {
            var values = new List<int?> { 1, 2 };
            var dh = DataTypeFactory.Match(typeof(int));
            var v = dh.InsertDefinitions(values, 1, new List<int> { 0, 1, 1, 0 }, out var _);

            Assert.Equal(4, v.Count);
            Assert.Equal(Nullable<int>(null, 1, 2, null), v);
        }

        List<T?> Nullable<T>(params object[] values) where T : struct
        {
            return values.Select(v => v == null ? null : new T?((T)v)).ToList();
        }
    }
}
