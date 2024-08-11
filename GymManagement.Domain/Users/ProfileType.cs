using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Users
{
    public class ProfileType(string name, int value) : SmartEnum<ProfileType>(name, value)
    {
        public static readonly ProfileType Admin = new(nameof(Admin), 0);
        public static readonly ProfileType Tranier = new(nameof(Tranier), 1);
        public static readonly ProfileType Participant = new(nameof(Participant), 3);
    }
}
