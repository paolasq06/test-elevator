using Application.Common.Auth;
using System;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        Guid? Id { get; }
        AuthUserInfo GetUserInfo();
    }
}
