using Pardisan.ViewModels.API.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IPropertyRepository
    {
        Task<object> GetAll();
        Task<int> Create(CreatePropertyVM input);
        Task Edit(EditPropertyVM input);
        Task<object> Detail(int id);
        Task Delete(int id);
        Task RemoveImageFromGallery(int id);
        Task<bool> DoesItExist(int id);
        Task<object> GetForDashboard();
        Task<object> Images(int id);
        Task AddItemToGallery(AddToGalleryVM input);
        Task SaveFullProperty(FullPropertyVM input);
        Task<FullPropertyVM> GetFullProperty(int id);
    }
}
