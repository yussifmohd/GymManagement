using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Authentication.Common
{
    public static class AuthenticationErrors
    {
        public static readonly Error InvalidCredentials = Error.Validation(
            code: "Authentication.InvalidCredentials",
            description: "Invalid credentials");
    }
}
