using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels.API.Question;
using System.Threading.Tasks;

namespace Pardisan.Services
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(CreateQuestionVM input)
        {
            var question = new Question
            {
                QuestionType = input.QuestionType,
                Tip = input.Tip,
                Title = input.Title,
                SurveyId = input.SurveyId
            };

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();

            foreach (var item in input.Options)
            {
                item.QuestionId = question.Id;
            }
            await _context.Options.AddRangeAsync(input.Options);
            await _context.SaveChangesAsync();
        }
        public async Task Edit(EditQuestionVM input)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == input.Id);

            question.Title = input.Title;
            question.Tip = input.Tip;

            _context.Update(question);
            await _context.SaveChangesAsync();

            foreach (var item in input.Options)
            {
                item.QuestionId = question.Id;
            }

            await _context.Options.AddRangeAsync(input.Options);

            //foreach (var item in input.DeletedOptions)
            //{
            //    var option = await _context.Questions.FirstOrDefaultAsync(x => x.Id == item.Id);
            //    option.IsActive = false;
            //}

            await _context.SaveChangesAsync();
        }
        public async Task<bool> DoesItExist(int id)
        {
            var result = await _context.Surveys.AnyAsync(x => x.IsActive.Value && x.Id == id);
            return result;
        }
        public async Task Delete(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == id);

            question.IsActive = false;
            _context.Update(question);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOption(int id)
        {
            var option = await _context.Options.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == id);

            option.IsActive = false;
            _context.Update(option);
            await _context.SaveChangesAsync();
        }
    }
}
