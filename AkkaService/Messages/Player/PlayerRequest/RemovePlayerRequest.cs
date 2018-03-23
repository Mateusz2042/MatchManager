using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Player.PlayerRequest
{
    public class RemovePlayerRequest
    {
        public string Id { get; set; }

        public RemovePlayerRequest(string id)
        {
            Id = id;
        }
    }
}
