using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Player.PlayerResponse
{
    public class GetPlayerByIdResponse
    {
        public GetPlayerItem Player { get; }

        public GetPlayerByIdResponse(GetPlayerItem player)
        {
            Player = player;
        }
    }
}
