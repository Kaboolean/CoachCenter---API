﻿using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Services.Users
{
    public interface IUsersService
    {
        Task<Guid> CreateAsync(CreateUserModel model);
        Task<List<ListUserModel>> GetAllAsync(ListUserQueryParameters queryParameters);

        Task DeleteAsync(Guid id);
    }
}
