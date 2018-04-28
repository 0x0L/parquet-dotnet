// <auto-generated/>
/**
 * Autogenerated by Thrift Compiler (0.11.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Thrift;
using Thrift.Collections;

using Thrift.Protocols;
using Thrift.Protocols.Entities;
using Thrift.Protocols.Utilities;
using Thrift.Transports;
using Thrift.Transports.Client;
using Thrift.Transports.Server;


namespace Parquet.Thrift
{

  /// <summary>
  /// Wrapper struct to specify sort order
  /// </summary>
  public partial class SortingColumn : TBase
  {

    /// <summary>
    /// The column index (in this row group) *
    /// </summary>
    public int Column_idx { get; set; }

    /// <summary>
    /// If true, indicates this column is sorted in descending order. *
    /// </summary>
    public bool Descending { get; set; }

    /// <summary>
    /// If true, nulls will come before non-null values, otherwise,
    /// nulls go at the end.
    /// </summary>
    public bool Nulls_first { get; set; }

    public SortingColumn()
    {
    }

    public SortingColumn(int column_idx, bool descending, bool nulls_first) : this()
    {
      this.Column_idx = column_idx;
      this.Descending = descending;
      this.Nulls_first = nulls_first;
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_column_idx = false;
        bool isset_descending = false;
        bool isset_nulls_first = false;
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.I32)
              {
                Column_idx = await iprot.ReadI32Async(cancellationToken);
                isset_column_idx = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.Bool)
              {
                Descending = await iprot.ReadBoolAsync(cancellationToken);
                isset_descending = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 3:
              if (field.Type == TType.Bool)
              {
                Nulls_first = await iprot.ReadBoolAsync(cancellationToken);
                isset_nulls_first = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
        if (!isset_column_idx)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
        if (!isset_descending)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
        if (!isset_nulls_first)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("SortingColumn");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        field.Name = "column_idx";
        field.Type = TType.I32;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(Column_idx, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        field.Name = "descending";
        field.Type = TType.Bool;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(Descending, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        field.Name = "nulls_first";
        field.Type = TType.Bool;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(Nulls_first, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString()
    {
      var sb = new StringBuilder("SortingColumn(");
      sb.Append(", Column_idx: ");
      sb.Append(Column_idx);
      sb.Append(", Descending: ");
      sb.Append(Descending);
      sb.Append(", Nulls_first: ");
      sb.Append(Nulls_first);
      sb.Append(")");
      return sb.ToString();
    }
  }

}
