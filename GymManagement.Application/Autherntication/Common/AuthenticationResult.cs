﻿using GymManagement.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Authentication.Common
{
    public record AuthenticationResult(User User, string Token);
    
}
