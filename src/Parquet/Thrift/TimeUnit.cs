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

  public partial class TimeUnit : TBase
  {
    private MilliSeconds _MILLIS;
    private MicroSeconds _MICROS;

    public MilliSeconds MILLIS
    {
      get
      {
        return _MILLIS;
      }
      set
      {
        __isset.MILLIS = true;
        this._MILLIS = value;
      }
    }

    public MicroSeconds MICROS
    {
      get
      {
        return _MICROS;
      }
      set
      {
        __isset.MICROS = true;
        this._MICROS = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool MILLIS;
      public bool MICROS;
    }

    public TimeUnit()
    {
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
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
              if (field.Type == TType.Struct)
              {
                MILLIS = new MilliSeconds();
                await MILLIS.ReadAsync(iprot, cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.Struct)
              {
                MICROS = new MicroSeconds();
                await MICROS.ReadAsync(iprot, cancellationToken);
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
        var struc = new TStruct("TimeUnit");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        if (MILLIS != null && __isset.MILLIS)
        {
          field.Name = "MILLIS";
          field.Type = TType.Struct;
          field.ID = 1;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await MILLIS.WriteAsync(oprot, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if (MICROS != null && __isset.MICROS)
        {
          field.Name = "MICROS";
          field.Type = TType.Struct;
          field.ID = 2;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await MICROS.WriteAsync(oprot, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
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
      var sb = new StringBuilder("TimeUnit(");
      bool __first = true;
      if (MILLIS != null && __isset.MILLIS)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("MILLIS: ");
        sb.Append(MILLIS== null ? "<null>" : MILLIS.ToString());
      }
      if (MICROS != null && __isset.MICROS)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("MICROS: ");
        sb.Append(MICROS== null ? "<null>" : MICROS.ToString());
      }
      sb.Append(")");
      return sb.ToString();
    }
  }

}