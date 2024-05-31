using Pardisan.ViewModels.API.PropertyOwner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IPropertyOwnerRepository
    {
        Task Create(CreatePropertyOwnerVM input);

        Task<bool> CheckPropertyOrUnitAlreadyHasOwner(CreatePropertyOwnerVM input);
        Task<object> List(int? propertyId = null);
        Task<object> Detail(int id);
        Task Edit(EditPropertyOwnerVM input);
        Task Delete(int id);
    }
}
