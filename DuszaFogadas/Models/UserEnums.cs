using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuszaFogadas.Models
{
    public class UserEnum
    {
        public enum UserRole
        {
            A = 0,
            SZ = 1,
            F = 2
        }

        public static UserRole ConvertEnum(string val)
        {
            switch (val) {
                case "A":
                    return UserRole.A;
                case "SZ":
                    return UserRole.SZ;
                case "F":
                    return UserRole.F;
                default:
                    return UserRole.F;
            }
        }
    }
}
