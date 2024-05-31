using Pardisan.ViewModels;
using Pardisan.ViewModels.DataTable;
using Pardisan.ViewModels.Message;
using Pardisan.ViewModels.Page;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IMessageRepository
    {
        Task<DatatableResponse> GetAllMessages(MessageDatatableInput input);
        Task<Response<MessageVM>> GetMessageById(int id);
    }
}



