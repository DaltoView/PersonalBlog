using BLL.DTO;
using BLL.DTO.Filters;
using BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        IEnumerable<UserDTO> GetAllUsers(UserFilterDTO userSearchDTO);
        UserDTO GetUserById(Guid id);
        OperationDetails CreateUser(UserDTO user);
        OperationDetails EditUser(UserDTO user);
        OperationDetails DeleteUser(Guid id);
    }
}
