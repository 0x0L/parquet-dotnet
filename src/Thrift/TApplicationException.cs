// Licensed to the Apache Software Foundation(ASF) under one
// or more contributor license agreements.See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied. See the License for the
// specific language governing permissions and limitations
// under the License.

using System.Threading;
using System.Threading.Tasks;
using Thrift.Protocols;
using Thrift.Protocols.Entities;

namespace Thrift
{
    // ReSharper disable once InconsistentNaming
    public class TApplicationException : TException
    {
        public enum ExceptionType
        {
            Unknown,
            UnknownMethod,
            InvalidMessageType,
            WrongMethodName,
            BadSequenceId,
            MissingResult,
            InternalError,
            ProtocolError,
            InvalidTransform,
            InvalidProtocol,
            UnsupportedClientType
        }

        private const int MessageTypeFieldId = 1;
        private const int ExTypeFieldId = 2;

        protected ExceptionType Type;

        public TApplicationException()
        {
        }

        public TApplicationException(ExceptionType type)
        {
            Type = type;
        }

        public TApplicationException(ExceptionType type, string message)
            : base(message)
        {
            Type = type;
        }

        public static async Task<TApplicationException> ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            string message = null;
            var type = ExceptionType.Unknown;

            await iprot.ReadStructBeginAsync(cancellationToken);
            while (true)
            {
                var field = await iprot.ReadFieldBeginAsync(cancellationToken);
                if (field.Type == TType.Stop)
                {
                    break;
                }

                switch (field.ID)
                {
                    case MessageTypeFieldId:
                        if (field.Type == TType.String)
                        {
                            message = await iprot.ReadStringAsync(cancellationToken);
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case ExTypeFieldId:
                        if (field.Type == TType.I32)
                        {
                            type = (ExceptionType)await iprot.ReadI32Async(cancellationToken);
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

            return new TApplicationException(type, message);
        }

        public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.FromCanceled(cancellationToken);
            }

            const string messageTypeFieldName = "message";
            const string exTypeFieldName = "exType";
            const string structApplicationExceptionName = "TApplicationException";

            var struc = new TStruct(structApplicationExceptionName);
            var field = new TField();

            await oprot.WriteStructBeginAsync(struc, cancellationToken);

            if (!string.IsNullOrEmpty(Message))
            {
                field.Name = messageTypeFieldName;
                field.Type = TType.String;
                field.ID = MessageTypeFieldId;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteStringAsync(Message, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }

            field.Name = exTypeFieldName;
            field.Type = TType.I32;
            field.ID = ExTypeFieldId;

            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await oprot.WriteI32Async((int)Type, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
            await oprot.WriteFieldStopAsync(cancellationToken);
            await oprot.WriteStructEndAsync(cancellationToken);
        }
    }
}
