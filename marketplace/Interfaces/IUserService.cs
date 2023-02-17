﻿using marketplace.Interfaces;
using marketplace.Models;
using marketplace.Resources;

namespace marketplace.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResponse> Register(RegisterRequest resource, CancellationToken cancellationToken);
        Task<UserResource> Login(LoginResource resource, CancellationToken cancellationToken);
        Task<bool> IsExistingEmail(string email);
    }
}
