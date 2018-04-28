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
  /// Integer logical type annotation
  /// 
  /// bitWidth must be 8, 16, 32, or 64.
  /// 
  /// Allowed for physical types: INT32, INT64
  /// </summary>
  public partial class IntType : TBase
  {

    public sbyte BitWidth { get; set; }

    public bool IsSigned { get; set; }

    public IntType()
    {
    }

    public IntType(sbyte bitWidth, bool isSigned) : this()
    {
      this.BitWidth = bitWidth;
      this.IsSigned = isSigned;
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_bitWidth = false;
        bool isset_isSigned = false;
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
              if (field.Type == TType.Byte)
              {
                BitWidth = await iprot.ReadByteAsync(cancellationToken);
                isset_bitWidth = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.Bool)
              {
                IsSigned = await iprot.ReadBoolAsync(cancellationToken);
                isset_isSigned = true;
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
        if (!isset_bitWidth)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
        if (!isset_isSigned)
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
        var struc = new TStruct("IntType");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        field.Name = "bitWidth";
        field.Type = TType.Byte;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteByteAsync(BitWidth, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        field.Name = "isSigned";
        field.Type = TType.Bool;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(IsSigned, cancellationToken);
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
      var sb = new StringBuilder("IntType(");
      sb.Append(", BitWidth: ");
      sb.Append(BitWidth);
      sb.Append(", IsSigned: ");
      sb.Append(IsSigned);
      sb.Append(")");
      return sb.ToString();
    }
  }

}
