﻿using System;
using System.Collections.Generic;
using System.Text;
using Parquet.Data;
using Xunit;

namespace Parquet.Tests
{
    public class MetadataTest
    {
        [Fact]
        public void Setting_custom_metadata_keys_reads_them_back()
        {
            var ds = new DataSet(new DataField<int>("id"));
            ds.Metadata.Custom["ivan"] = "is cool";

            DataSet ds1 = ds.WriteRead();

            Assert.Equal("is cool", ds1.Metadata.Custom["ivan"]);
        }
    }
}
