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

  public partial class PageLocation : TBase
  {

    /// <summary>
    /// Offset of the page in the file *
    /// </summary>
    public long Offset { get; set; }

    /// <summary>
    /// Size of the page, including header. Sum of compressed_page_size and header
    /// length
    /// </summary>
    public int Compressed_page_size { get; set; }

    /// <summary>
    /// Index within the RowGroup of the first row of the page; this means pages
    /// change on record boundaries (r = 0).
    /// </summary>
    public long First_row_index { get; set; }

    public PageLocation()
    {
    }

    public PageLocation(long offset, int compressed_page_size, long first_row_index) : this()
    {
      this.Offset = offset;
      this.Compressed_page_size = compressed_page_size;
      this.First_row_index = first_row_index;
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_offset = false;
        bool isset_compressed_page_size = false;
        bool isset_first_row_index = false;
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
              if (field.Type == TType.I64)
              {
                Offset = await iprot.ReadI64Async(cancellationToken);
                isset_offset = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.I32)
              {
                Compressed_page_size = await iprot.ReadI32Async(cancellationToken);
                isset_compressed_page_size = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 3:
              if (field.Type == TType.I64)
              {
                First_row_index = await iprot.ReadI64Async(cancellationToken);
                isset_first_row_index = true;
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
        if (!isset_offset)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
        if (!isset_compressed_page_size)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
        if (!isset_first_row_index)
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
        var struc = new TStruct("PageLocation");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        field.Name = "offset";
        field.Type = TType.I64;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI64Async(Offset, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        field.Name = "compressed_page_size";
        field.Type = TType.I32;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI32Async(Compressed_page_size, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        field.Name = "first_row_index";
        field.Type = TType.I64;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI64Async(First_row_index, cancellationToken);
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
      var sb = new StringBuilder("PageLocation(");
      sb.Append(", Offset: ");
      sb.Append(Offset);
      sb.Append(", Compressed_page_size: ");
      sb.Append(Compressed_page_size);
      sb.Append(", First_row_index: ");
      sb.Append(First_row_index);
      sb.Append(")");
      return sb.ToString();
    }
  }

}