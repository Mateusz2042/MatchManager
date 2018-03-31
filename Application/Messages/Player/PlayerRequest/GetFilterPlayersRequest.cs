using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Player.PlayerRequest
{
    public class GetFilterPlayersRequest
    {
        public string Text { get; set; }

        public GetFilterPlayersRequest(string text)
        {
            Text = text;
        }
    }
}
