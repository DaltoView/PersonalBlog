using BLL.DTO;
using BLL.Infrastructure;
using System;
using System.Security.Claims;

namespace BLL.Interfaces
{
    public interface IAccountService : IDisposable
    {
        OperationDetails RegisterUser(UserDTO user);
        UserDTO FindUser(string userName, string password);
        OperationDetails EditUser(UserDTO user);
        string GetRoleByUserId(Guid userId);
        ClaimsIdentity CreateIdentity(Guid userId, string authenticationType);
        UserDTO GetUserById(Guid userId);
        UserDTO GetUserByName(string userName);
        bool IsInRole(Guid id, string role);
        OperationDetails DeleteUser(Guid id);
        OperationDetails DeleteUser(string userName);
    }
}
