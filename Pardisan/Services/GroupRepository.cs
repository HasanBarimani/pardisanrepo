using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;
using Pardisan.ViewModels.API.Group;
using Pardisan.Models;

namespace Pardisan.Services
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetAll()
        {
            var data = await _context.Groups.Where(x => x.IsActive.Value).Include(x => x.Members).Select(x => new
            {
                x.Id,
                x.Title,
                MemberCount = x.Members.Count,
                CreatedAt = x.CreatedAt.ToPersianDateTextify(false)
            }).ToListAsync();

            return data;
        }

        public async Task<object> Create(CreateGroupVM input)
        {
            var group = new Group
            {
                Title = input.Title
            };
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return group.Id;
        }
        public async Task<bool> DoesItExist(int id)
        {
            var result = await _context.Groups.AnyAsync(x => x.IsActive.Value && x.Id == id);
            return result;
        }
        public async Task<object> Detail(int id)
        {
            var data = await _context.Groups
                .Where(x => x.IsActive.Value && x.Id == id)
                .Include(x => x.Members).ThenInclude(x => x.Owner)
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    MemberCount = x.Members.Count,
                    Members = x.Members.Select(y => new
                    {
                        y.Id,
                        y.GroupId,
                        y.OwnerId,
                        Owner = new
                        {
                            y.Owner.FirstName,
                            y.Owner.LastName,
                            y.Owner.PhoneNumber
                        },
                        CreatedAt = y.CreatedAt.ToPersianDateTextify(false),
                    }).ToList(),
                    CreatedAt = x.CreatedAt.ToPersianDateTextify(false)

                }).FirstOrDefaultAsync();

            return data;
        }

        public async Task Edit(EditGroupVM input)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == input.Id);

            group.Title = input.Title;
            _context.Update(group);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == id);

            group.IsActive = false;
            _context.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task AddMember(AddMemberVM input)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == input.GroupId);

            var member = new Member
            {
                GroupId = group.Id,
                OwnerId = input.OwnerId,
            };
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveMember(AddMemberVM input)
        {
            var member = await _context.Members
              .FirstOrDefaultAsync(x => x.IsActive.Value
              && x.GroupId == input.GroupId
              && x.OwnerId == input.OwnerId
              );

            member.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsOwnerAMemberOfTheGroup(AddMemberVM input)
        {
            var member = await _context.Members
                .FirstOrDefaultAsync(x => x.IsActive.Value
                && x.GroupId == input.GroupId
                && x.OwnerId == input.OwnerId
                );

            bool result = member == null ?  false : true;

            return result;
        }
    }
}
