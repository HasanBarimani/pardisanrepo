using Pardisan.Models;
using Pardisan.ViewModels.API.Survey;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface ISurveyRepository
    {
        Task<object> GetAll();
        Task<int> Create(CreateSurveyVM input);
        Task<object> Detail(int id);
        Task<bool> DoesItExist(int id);
        Task Edit(EditSurveyVM input);
        Task Delete(int id);
        Task<object> CheckSurveyValidity(string code);
        Task<object> DetailWithAnswers(int id);
        Task<object> DetailWithAnswers(int id, string code);
        Task<object> GetForDashboard();
        Task<bool> Answer(SubmitAnswerVM input);
    }
}
