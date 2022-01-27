﻿using FragmentNetslumServer.Entities.Attributes;
using FragmentNetslumServer.Services;
using FragmentNetslumServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static FragmentNetslumServer.Services.Extensions;

namespace FragmentNetslumServer.Entities.OpCodeHandlers.Data.Guild
{
    [OpCodeData(OpCodes.OPCODE_DATA_GUILD_GET_INFO)]
    public sealed class OPCODE_DATA_GUILD_GET_INFO : IOpCodeHandler
    {
        public Task<IEnumerable<ResponseContent>> HandleIncomingRequestAsync(RequestContent request)
        {
            var guildId = swap16(BitConverter.ToUInt16(request.Data, 0));
            return Task.FromResult<IEnumerable<ResponseContent>>(
                new[]
                {
                    request.CreateResponse(OpCodes.OPCODE_DATA_GET_GUILD_INFO_RESPONSE, GuildManagementService.GetInstance().GetGuildInfo(guildId))
                });
        }
    }
}
