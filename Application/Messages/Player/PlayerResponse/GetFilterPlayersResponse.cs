using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Messages.Player.PlayerResponse
{
    public class GetFilterPlayersResponse
    {
        public ICollection<GetPlayerItem> Players { get; }

        public GetFilterPlayersResponse(IEnumerable<GetPlayerItem> players)
        {
            Players = players.ToList();
        }
    }
}
