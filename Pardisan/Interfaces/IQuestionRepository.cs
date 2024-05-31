using Pardisan.ViewModels.API.Question;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IQuestionRepository
    {
        Task Create(CreateQuestionVM input);
        Task<bool> DoesItExist(int id);
        Task Delete(int id);
        Task Edit(EditQuestionVM input);
        Task DeleteOption(int id);
    }
}
