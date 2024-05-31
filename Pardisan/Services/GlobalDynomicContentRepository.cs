using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels.Holding;
using Pardisan.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Pardisan.ViewModels.DynamicContent;
using System.Net;
using System;
using Microsoft.EntityFrameworkCore;

namespace Pardisan.Services
{
    public class GlobalDynomicContentRepository : IGlobalDynomicContent
    {
        private readonly ApplicationDbContext _context;
        public GlobalDynomicContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Response<GlobalDynomicContentVM> GetAll()
        {
            var data = _context.Settings.AsNoTracking();
            var visit = _context.SiteVisit.AsNoTracking();
            var pageData = _context.Pages.Where(x => x.IsActive == true).Select(x => new PageVm()
            {
                Content = x.Content,
                Title = x.Title,
                Url = x.Url,

            }).AsNoTracking();

            var contentVM = new GlobalDynomicContentVM()
            {
                WorkTime = data.FirstOrDefault(x => x.Key == "WorkTime").Value,
                Phone = data.FirstOrDefault(x => x.Key == "Phone").Value,
                Cellphone = data.FirstOrDefault(x => x.Key == "Cellphone").Value,
                Email = data.FirstOrDefault(x => x.Key == "Email").Value,
                Whatsapp = data.FirstOrDefault(x => x.Key == "Whatsapp").Value,
                Twitter = data.FirstOrDefault(x => x.Key == "Twitter").Value,
                Telegram = data.FirstOrDefault(x => x.Key == "Telegram").Value,
                Instagram = data.FirstOrDefault(x => x.Key == "Instagram").Value,
                Address = data.FirstOrDefault(x => x.Key == "Address").Value,
                flinkName = data.FirstOrDefault(x => x.Key == "FirstLinkName").Value,
                flink = data.FirstOrDefault(x => x.Key == "FirstLink").Value,
                slinkName = data.FirstOrDefault(x => x.Key == "SecondLinkName").Value,
                slink = data.FirstOrDefault(x => x.Key == "SecondLink").Value,
                tlinkName = data.FirstOrDefault(x => x.Key == "ThirdLinkName").Value,
                tlink = data.FirstOrDefault(x => x.Key == "ThirdLink").Value,
                ProjectPic = data.FirstOrDefault(x => x.Key == "ProjectPic").Value,
                titleForVideos = data.FirstOrDefault(x => x.Key == "titleForVideos").Value,
                titleForHolding = data.FirstOrDefault(x => x.Key == "titleForHolding").Value,
                dayCountVisit = visit.Where(x => x.Date.Date == DateTime.Today).Count(),
                weekCountVisit = visit.Where(x => x.Date.Date >= DateTime.Now.AddDays(-7)).Count(),
                monthCountVisit = visit.Where(x => x.Date.Date >= DateTime.Now.AddMonths(-1)).Count(),
                page = pageData.ToList()
            };

            return new Response<GlobalDynomicContentVM>(contentVM);
        }

    }
}
