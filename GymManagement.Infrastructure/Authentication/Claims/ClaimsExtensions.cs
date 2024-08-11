using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Authentication.Claims
{
    public static class ClaimsExtensions
    {
        public static List<Claim> AddIfValueNotNull(this List<Claim> claims, string type, string? value)
        {
            if(value is not null)
            {
                claims.Add(new Claim(type: type, value: value));
            }

            return claims;
        }
    }
}
