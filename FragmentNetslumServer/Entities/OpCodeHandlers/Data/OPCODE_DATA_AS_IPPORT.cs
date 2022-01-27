﻿using FragmentNetslumServer.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FragmentNetslumServer.Entities.OpCodeHandlers.Data
{
    [OpCodeData(OpCodes.OPCODE_DATA_AS_IPPORT), Description("Determines the external IP address for the Client. This does also attempt to handle rewriting 127.0.0.1 to a 'more correct' IP address")]
    public sealed class OPCODE_DATA_AS_IPPORT : SimpleResponseOpCodeHandler
    {
        public OPCODE_DATA_AS_IPPORT() : base(OpCodes.OPCODE_DATA_AS_IPPORT_OK, new byte[] { 0x00, 0x00 }) { }

        public override Task<IEnumerable<ResponseContent>> HandleIncomingRequestAsync(RequestContent request)
        {
            request.Client.ipdata = request.Data;
            var externalIpAddress = request.Client.ipEndPoint.Address.ToString();
            if (externalIpAddress == Helpers.IPAddressHelpers.LOOPBACK_IP_ADDRESS)
            {
                externalIpAddress = Helpers.IPAddressHelpers.GetLocalIPAddress2();
            }
            var ipAddress = externalIpAddress.Split('.');
            var ipAddressBytes = ipAddress.Reverse().Select(c => byte.Parse(c)).ToArray();
            request.Client.externalIPAddress = ipAddressBytes;
            return base.HandleIncomingRequestAsync(request);
        }
    }
}
