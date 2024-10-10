using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DuszaFogadas.Models.UserEnum;

namespace DuszaFogadas.Models
{
    public class User
    {
        public User(int id, string name, int points, UserRole role)
        {
            Id = id;
            Name = name;
            Points = points;
            Role = role;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public UserRole Role { get; set; }
    }
}
