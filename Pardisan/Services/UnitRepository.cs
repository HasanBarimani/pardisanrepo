using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;
using Pardisan.ViewModels.API.Unit;
using Pardisan.Models;

namespace Pardisan.Services
{
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext _context;

        public UnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetByProperty(int propertyId)
        {
            //var ownerships = _context.PropertyOwners.Include(x=>x.Owner).Where(x => x.IsActive.Value).ToList();
            var data = await _context.Units.Where(x => x.IsActive.Value && x.PropertyId == propertyId).Include(x=>x.Owners).Select(x => new
            {
                x.Id,
                x.PropertyId,
                x.Floor,
                x.Number,
                x.Meterage,
                x.OwnerId,
                //Owner = new
                //{
                //    FirstName = x.Owner != null ? x.Owner.FirstName : "",
                //    LastName = x.Owner != null ? x.Owner.LastName : "",
                //    PhoneNumber = x.Owner != null ? x.Owner.PhoneNumber : "",
                //},
                CreatedAt = x.CreatedAt.ToPersianDateTextify(false)
            }).ToListAsync();

            return data;
        }
        public async Task<int> Create(CreateUnitVM input)
        {
            var unit = new Unit
            {
                Floor = input.Floor,
                Meterage = input.Meterage,
                Number = input.Number,
                PropertyId = input.PropertyId,
                OwnerId = input.OwnerId,
            };
            await _context.Units.AddAsync(unit);
            await _context.SaveChangesAsync();

            return unit.Id;
        }

        public async Task<object> Detail(int id)
        {
            var ownership = _context.PropertyOwners.FirstOrDefault(x => x.IsActive.Value && x.UnitId == id);
            var data = await _context.Units.Where(x => x.IsActive.Value && x.Id == id).Select(x => new
            {
                x.Id,
                x.PropertyId,
                x.Number,
                x.Floor,
                x.Meterage,
                x.OwnerId,
                ownership,
                //Owner = new
                //{
                //    FirstName = x.Owner != null ? x.Owner.FirstName : "",
                //    LastName = x.Owner != null ? x.Owner.LastName : "",
                //    PhoneNumber = x.Owner != null ? x.Owner.PhoneNumber : "",
                //},
                CreatedAt = x.CreatedAt.ToPersianDateTextify(false)
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task Edit(EditUnitVM input)
        {
            var unit = await _context.Units.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == input.Id);

            unit.Meterage = input.Meterage;
            unit.Floor = input.Floor;
            unit.Number = input.Number;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DoesItExist(int id)
        {
            var result = await _context.Units.AnyAsync(x => x.IsActive.Value && x.Id == id);
            return result;
        }

        public async Task Delete(int id)
        {
            var unit = await _context.Units.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == id);

            unit.IsActive = false;
            _context.Update(unit);
            await _context.SaveChangesAsync();
        }
    }
}
