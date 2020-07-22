using System;

namespace bSharpUILib.Models
{
    public class GameModel
    {
        public string GameName { get; set; }
        public string GameUrl { get; set; }

        public GameModel(string gameName, string gameUrl)
        {
            this.GameName = gameName;
            this.GameUrl = gameUrl;
        }
    }
}
