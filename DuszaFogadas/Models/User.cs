using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuszaFogadas.Models
{
    internal class User
    {
        public User(int id, string name, int points, string role)
        {
            Id = id;
            Name = name;
            Points = points;
            Role = role;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public string Role { get; set; }
    }
}
