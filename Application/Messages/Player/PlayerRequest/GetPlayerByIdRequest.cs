using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Player.PlayerRequest
{
    public class GetPlayerByIdRequest
    {
        public string Id { get; }

        public GetPlayerByIdRequest(string id)
        {
            Id = id;
        }
    }
}
