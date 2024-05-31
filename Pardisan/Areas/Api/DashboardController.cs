using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Api
{
    [Route("api/crm/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DashboardController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly ApplicationDbContext _context;

        public DashboardController(IPropertyRepository propertyRepository, IOwnerRepository ownerRepository, ISurveyRepository surveyRepository, ApplicationDbContext context)
        {
            _propertyRepository = propertyRepository;
            _ownerRepository = ownerRepository;
            _surveyRepository = surveyRepository;
            _context = context;
        }

        [HttpGet("Dashboard")]
        public async Task<IActionResult> Index()
        {
            var thisWeekDate = DateTime.Now.GetPersianWeekStartAndEndDates(true).StartDate.Date;
            var thisMonthDate = DateTime.Now.GetPersianMonthStartAndEndDates(true).StartDate.Date;
            var lastMonthDate = DateTime.Now.AddMonths(-1).GetPersianMonthStartAndEndDates(true).StartDate.Date;

            var properties = _context.Properties.Where(x => x.IsActive.Value).AsNoTracking();

            var thisMonthPropertiesCount = properties.Where(w => w.CreatedAt.Date >= thisMonthDate).Count();
            var lastMonthPropertiesCount = properties.Where(w => w.CreatedAt.Date >= lastMonthDate && w.CreatedAt < thisMonthDate).Count();
            var totalPropertiesCount = properties.Count();

            var propertiesRatio = 0;
            if (thisMonthPropertiesCount > 0)
            {
                propertiesRatio = (int)(lastMonthPropertiesCount > 0 ? (thisMonthPropertiesCount - lastMonthPropertiesCount) / lastMonthPropertiesCount * 100 : thisMonthPropertiesCount * 100);
            }

            var thisWeekProperties = await properties.Where(w => w.CreatedAt.Date >= thisWeekDate).CountAsync();
            var thisDayProperties = await properties.Where(w => w.CreatedAt.Date == DateTime.Now).CountAsync();
            var lastDayProperties = await properties.Where(w => w.CreatedAt.Date == DateTime.Now.AddDays(-1)).CountAsync();


            var owners = _context.Owners.Where(x => x.IsActive.Value).AsNoTracking();

            double thisMonthOwnerCount = await owners.Where(w => w.CreatedAt.Date >= thisMonthDate).CountAsync();
            double lastMonthOwnerCount = await owners.Where(w => w.CreatedAt.Date >= lastMonthDate && w.CreatedAt < thisMonthDate).CountAsync();
            var totalOwner = owners.Count();
            var ownersRatio = 0;
            if (thisMonthOwnerCount > 0)
            {
                ownersRatio = (int)(lastMonthOwnerCount > 0 ? (thisMonthOwnerCount - lastMonthOwnerCount) / lastMonthOwnerCount * 100 : thisMonthOwnerCount * 100);
            }

            var thisWeekOwner = await owners.Where(w => w.CreatedAt.Date >= thisWeekDate).CountAsync();
            var thisDayOwner = await owners.Where(w => w.CreatedAt.Date == DateTime.Now).CountAsync();
            var lastDayOwner = await owners.Where(w => w.CreatedAt.Date == DateTime.Now.AddDays(-1)).CountAsync();


            var surveys = _context.Surveys.Where(w => w.IsActive.Value).AsNoTracking();

            double thisMonthSurveyCount = await surveys.Where(w => w.CreatedAt.Date >= thisMonthDate).CountAsync();
            double lastMonthSurveyCount = await surveys.Where(w => w.CreatedAt.Date >= lastMonthDate && w.CreatedAt < thisMonthDate).CountAsync();

            var totalSurveys = surveys.Count();
            var surveyRatio = 0;
            if (thisMonthSurveyCount > 0)
            {
                surveyRatio = (int)(lastMonthSurveyCount > 0 ? (thisMonthSurveyCount - lastMonthSurveyCount) / lastMonthSurveyCount * 100 : thisMonthSurveyCount * 100);
            }

            var thisWeekSurvey = await surveys.Where(w => w.CreatedAt.Date >= thisWeekDate).CountAsync();
            var thisDaySurvey = await surveys.Where(w => w.CreatedAt.Date == DateTime.Now).CountAsync();
            var lastDaySurvey = await surveys.Where(w => w.CreatedAt.Date == DateTime.Now.AddDays(-1)).CountAsync();



            var result = new
            {
                property = new
                {
                    thisMonthPropertiesCount,
                    lastMonthPropertiesCount,
                    totalPropertiesCount,
                    propertiesRatio,
                    thisWeekProperties,
                    thisDayProperties,
                    lastDayProperties
                },
                owner = new
                {
                    thisMonthOwnerCount,
                    lastMonthOwnerCount,
                    totalOwner,
                    ownersRatio,
                    thisWeekOwner,
                    thisDayOwner,
                    lastDayOwner
                },

                survey = new
                {
                    thisMonthSurveyCount,
                    lastMonthSurveyCount,
                    totalSurveys,
                    surveyRatio,
                    thisWeekSurvey,
                    thisDaySurvey,
                    lastDaySurvey
                }

                //owners = await _ownerRepository.GetForDashboard(),
                //properties = await _propertyRepository.GetForDashboard(),
                //surveys = await _surveyRepository.GetForDashboard()
            };
            return Ok(new JsonResponse(Pardisan.Data.StatusCode.OK, "با موفقیت انجام شد", new List<string>(), result));
        }
    }
}
