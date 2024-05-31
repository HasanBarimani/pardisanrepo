using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Exceptions;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels.API.Survey;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Services
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly ApplicationDbContext _context;

        public SurveyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetAll()
        {
            return await _context.Surveys.Where(x => x.IsActive.Value).Include(x => x.Property).Include(x=>x.Owners).OrderByDescending(x => x.UpdatedAt).Select(x => new
            {
                x.Id,
                x.Title,
                x.Describtion,
                x.AnswerCount,
                Property = new
                {
                    x.Property.Id,
                    x.Property.Title,
                    x.Property.Image,
                    x.Property.Address
                },
                OwnersCount = x.Owners.Where(x => x.IsActive.Value).Count(),
                OwnersWithAnswerCount = x.Owners.Where(x=>x.IsActive.Value && x.HasAnswered).Count(),
                x.PropertyId,
                UpdatedAt = x.UpdatedAt.ToShortPersianDateString(false),
                Questions = new List<object>(),
            }).ToListAsync();
        }

        public async Task<int> Create(CreateSurveyVM input)
        {
            var survey = new Survey
            {
                Describtion = input.Describtion,
                Title = input.Title,
                PropertyId = input.PropertyId
            };

            await _context.AddAsync(survey);
            await _context.SaveChangesAsync();

            foreach (var item in input.Owners)
            {
                var surveyOwner = new SurveyOwner
                {
                    OwnerId = item.OwnerId,
                    SurveyId = survey.Id,
                    Code = "S" + survey.Id + input.PropertyId + item.OwnerId
                };
                await _context.SurveyOwners.AddAsync(surveyOwner);
            }

            await _context.SaveChangesAsync();

            return survey.Id;
        }

        public async Task<object> Detail(int id)
        {
            var survey = await _context.Surveys
                .Where(x => x.IsActive.Value && x.Id == id)
                .Include(x => x.Property)
                .Include(x => x.Owners).ThenInclude(x => x.Owner)
                .Include(x => x.Questions).ThenInclude(x => x.Options)
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Describtion,
                    x.AnswerCount,
                    Property = new
                    {
                        x.Property.Id,
                        x.Property.Title,
                        x.Property.Image,
                        x.Property.Address
                    },
                    x.PropertyId,
                    UpdatedAt = x.UpdatedAt.ToPersianDateTextify(false),
                    Questions = x.Questions.Where(y => y.IsActive.Value).Select(y => new
                    {
                        y.Id,
                        y.SurveyId,
                        y.Title,
                        y.Tip,
                        y.QuestionType,
                        Options = y.Options.Where(z => z.IsActive.Value).Select(z => new
                        {
                            z.Id,
                            z.Title
                        })
                    }),
                    Owners = x.Owners.Where(y => y.IsActive.Value).Select(y => new
                    {
                        y.OwnerId,
                        Owner = new
                        {
                            y.Owner.FirstName,
                            y.Owner.LastName,
                            y.Owner.PhoneNumber,
                        },
                        y.Code,
                        y.HasAnswered,
                    })
                }).FirstOrDefaultAsync();

            return survey;
        }

        public async Task<object> DetailWithAnswers(int id, string code)
        {
            var survey = await _context.Surveys
                .Where(x => x.IsActive.Value && x.Id == id)
                .Include(x => x.Owners)
                .ThenInclude(x => x.Answers)
                .Include(x => x.Property)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Options).Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Describtion,
                    x.Owners.FirstOrDefault(y => y.Code == code).HasAnswered,
                    Property = new
                    {
                        x.Property.Id,
                        x.Property.Title,
                        x.Property.Image,
                        x.Property.Address
                    },
                    x.PropertyId,
                    Questions = x.Questions.Where(y => y.IsActive.Value).Select(y => new
                    {
                        y.Id,
                        y.Title,
                        y.Tip,
                        y.QuestionType,
                        Options = y.Options.Where(z => z.IsActive.Value).Select(z => new
                        {
                            z.Id,
                            z.Title
                        }),
                        HasAnswered = x.Owners.FirstOrDefault(z => z.Code == code).Answers.FirstOrDefault(a => a.QuestionId == y.Id) == null ? false : true,
                        Answer = x.Owners.FirstOrDefault(z => z.Code == code).Answers.FirstOrDefault(a => a.QuestionId == y.Id) == null
                        ? null : x.Owners.FirstOrDefault(z => z.Code == code).Answers.FirstOrDefault(a => a.QuestionId == y.Id).ChosenOptions,
                    })
                }).FirstOrDefaultAsync();

            return survey;
        }

        public async Task<object> DetailWithAnswers(int id)
        {
            var survey = await _context.Surveys
                .Where(x => x.IsActive.Value && x.Id == id)
                .Include(x => x.Owners)
                .ThenInclude(x => x.Answers)
                .Include(x => x.Property)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Options).Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Describtion,
                    Property = new
                    {
                        x.Property.Id,
                        x.Property.Title,
                        x.Property.Image,
                        x.Property.Address
                    },
                    x.PropertyId,
                    Questions = x.Questions.Where(y => y.IsActive.Value).Select(y => new
                    {
                        y.Id,
                        y.Title,
                        y.Tip,
                        y.QuestionType,
                        Options = y.Options.Where(z => z.IsActive.Value).Select(z => new
                        {
                            z.Id,
                            z.Title
                        }),

                        Answers = x.Owners.Where(z => z.HasAnswered).Select(z => new
                        {
                            ChosenOptions = z.Answers.FirstOrDefault(a => a.QuestionId == y.Id) == null
                            ? null : z.Answers.FirstOrDefault(a => a.QuestionId == y.Id).ChosenOptions,
                        }),
                        //Answers.FirstOrDefault(a => a.QuestionId == y.Id) == null
                        //? null : x.Owners.FirstOrDefault(z => z.Code == code).Answers.FirstOrDefault(a => a.QuestionId == y.Id).ChosenOptions,
                    })
                }).FirstOrDefaultAsync();

            return survey;
        }

        public async Task<bool> DoesItExist(int id)
        {
            var result = await _context.Surveys.AnyAsync(x => x.IsActive.Value && x.Id == id);
            return result;
        }

        public async Task Edit(EditSurveyVM input)
        {
            var survey = await _context.Surveys.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == input.Id);

            survey.Title = input.Title;
            survey.Describtion = input.Describtion;
            _context.Update(survey);

            if (input.Owners != null)
            {
                if (input.Owners.Count != 0)
                {
                    foreach (var item in input.Owners)
                    {
                        if (!item.IsForDelete)
                        {
                            var code = "S" + survey.Id + survey.PropertyId + item.OwnerId;
                            if (!_context.SurveyOwners.Where(x => x.IsActive.Value && x.Code == code).Any())
                            {
                                var surveyOwner = new SurveyOwner
                                {
                                    OwnerId = item.OwnerId,
                                    SurveyId = survey.Id,
                                    Code = code
                                };
                                await _context.SurveyOwners.AddAsync(surveyOwner);
                            }
                        }
                        else
                        {
                            var data = _context.SurveyOwners.FirstOrDefault(x => x.OwnerId == item.OwnerId && x.SurveyId == survey.Id);
                            if (data != null)
                                data.IsActive = false;
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var survey = await _context.Surveys.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == id);

            survey.IsActive = false;
            _context.Update(survey);
            await _context.SaveChangesAsync();
        }

        public async Task<object> CheckSurveyValidity(string code)
        {
            var surveyOwner = await _context.SurveyOwners.FirstOrDefaultAsync(x => x.IsActive.Value && x.Code == code);
            if (surveyOwner != null)
            {
                return new { isValid = true, surveyOwner.SurveyId };
            }
            else
            {
                return new { isValid = false, SurveyId = false };
            }
        }

        public async Task<object> GetForDashboard()
        {
            var data = await _context.Surveys.Where(x => x.IsActive.Value).ToListAsync();
            return new
            {
                LastFive = data.OrderByDescending(x => x.Id),
                Count = data.Count
            };
        }

        public async Task<bool> Answer(SubmitAnswerVM input)
        {
            var surveyOwner = await _context.SurveyOwners.FirstOrDefaultAsync(x => x.IsActive.Value && x.Code == input.Code && x.SurveyId == input.SurveyId);

            if (surveyOwner == null)
                return false;

            foreach (var item in input.QuestionAnswers)
            {
                var answerFromDb = _context.Answers.Where(x => x.IsActive.Value &&
                    x.QuestionId == item.QuestionId &&
                    x.OwnerId == surveyOwner.OwnerId &&
                    x.SurveyId == input.SurveyId
                ).FirstOrDefault();

                if (answerFromDb != null)
                {
                    answerFromDb.ChosenOptions = item.ChosenOptions;
                }
                else
                {
                    var answer = new Answer
                    {
                        SurveyOwnerId = surveyOwner.Id,
                        OwnerId = surveyOwner.OwnerId,
                        ChosenOptions = item.ChosenOptions,
                        QuestionId = item.QuestionId,
                        SurveyId = input.SurveyId
                    };
                    await _context.Answers.AddAsync(answer);
                }
            }
            surveyOwner.HasAnswered = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
