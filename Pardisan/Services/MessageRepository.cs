using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels;
using Pardisan.ViewModels.DataTable;
using Pardisan.ViewModels.Message;
using Pardisan.ViewModels.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Services
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DatatableResponse> GetAllMessages(MessageDatatableInput input)
        {

            var findMessages = _context.Messages.AsQueryable();

            if (input.Deleted == true)
            {
                findMessages = findMessages.Where(w => w.IsActive == false);
            }
            else
            {
                findMessages = findMessages.Where(w => w.IsActive == true);
            }

            if (!string.IsNullOrEmpty(input.Search.Value))
            {
                findMessages = findMessages.Where(w =>
                    string.IsNullOrEmpty(w.Name) || w.Name.Contains(input.Search.Value)
                );
            }

            var query = findMessages.Select(x => new MessageDatatableResult()
            {
                Content = x.Content,
                Email = x.Email,
                Name = x.Name,
                Id = x.Id,
                IsActive = x.IsActive

            }).OrderByDescending(x => x.Id).AsQueryable();

            DatatableResponse response = new DatatableResponse
            {
                ITotalDisplayRecords = findMessages.Count(),
                ITotalRecords = _context.Messages.Count(),
                AaData = await findMessages.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }

        public async Task<Response<MessageVM>> GetMessageById(int messageId)
        {
            try
            {
                var messageObj = await _context.Messages.FirstOrDefaultAsync(d => d.Id == messageId);
                if (messageObj == null)
                {
                    return new Response<MessageVM>(404);
                }
                return new Response<MessageVM>(CreateViewModelFromNews(messageObj));
            }
            catch (Exception)
            {
                return new Response<MessageVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }
        private MessageVM CreateViewModelFromNews(Message message)
        {
            var viewModel = new MessageVM()
            {
                Id = message.Id,
                Name = message.Name,

                Content = message.Content,
                Email = message.Email,
                IsActive = message.IsActive,
                CreatedAt = message.CreatedAt,

            };

            return viewModel;
        }
    }
}
