using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuszaFogadas.Models
{
    public class Game
    {
        public int id { get; }

        public Game(int id, string hostName, string gameName, List<string> participants, List<string> events)
        {
            this.id = id;
            HostName = hostName;
            GameName = gameName;
            Participants = participants;
            Events = events;
        }

        
        public string HostName { get; set; }
        public string GameName { get; set; }
        public List<string> Participants { get; set; } 
        public List<string> Events { get; set; }
    }
}
