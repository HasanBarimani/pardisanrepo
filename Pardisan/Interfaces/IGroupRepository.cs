using Pardisan.ViewModels.API.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IGroupRepository
    {
        Task<object> GetAll();
        Task<object> Create(CreateGroupVM input);
        Task Edit(EditGroupVM input);
        Task<object> Detail(int id);
        Task<bool> DoesItExist(int id);
        Task Delete(int id);
        Task AddMember(AddMemberVM input);
        Task RemoveMember(AddMemberVM input);
        Task<bool> IsOwnerAMemberOfTheGroup(AddMemberVM input);
    }
}
