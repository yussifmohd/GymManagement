﻿using GymManagement.Domain.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface IAdminRepository
    {
        Task AddAdminAsync(Admin admin);
        Task<Admin?> GetByIdAsync(Guid adminId);
        Task UpdateAsync (Admin admin);
    }
}
