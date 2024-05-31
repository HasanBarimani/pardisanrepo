using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels.API.PropertyOwner;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;

namespace Pardisan.Services
{
    public class PropertyOwnerRepository : IPropertyOwnerRepository
    {
        private readonly ApplicationDbContext _context;

        public PropertyOwnerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckPropertyOrUnitAlreadyHasOwner(CreatePropertyOwnerVM input)
        {
            if (input.UnitId == null)
            {
                //return await _context.PropertyOwners.Where(x => x.IsActive.Value && x.PropertyId == input.PropertyId).AnyAsync();
                return false;
            }
            else
            {
                return await _context.PropertyOwners.Where(x => x.IsActive.Value && x.UnitId == input.UnitId).AnyAsync();
            }
        }

        public async Task Create(CreatePropertyOwnerVM input)
        {
            DateTime date = DateTime.Now;

            if(input.ContractSigningDate != null && input.ContractSigningDate != DateTime.MinValue)
            {
                PersianCalendar pc = new PersianCalendar();
                date = new DateTime(input.ContractSigningDate.Year, input.ContractSigningDate.Month, input.ContractSigningDate.Day, pc);
            }

            var propertyOwner = new PropertyOwner
            {
                ContractSigningDate = date,
                Description = input.Description,
                DisSatisfactionLevelReason = input.DisSatisfactionLevelReason,
                HasDesireToIntroduce = input.HasDesireToIntroduce,
                HasIntroductionLeadsToPurchase = input.HasIntroductionLeadsToPurchase,
                OwnerId = input.OwnerId,
                PropertyId = input.PropertyId,
                SatisfactionLevel = input.SatisfactionLevel,
                UnitId = input.UnitId,
                IsUnitOwnership = input.UnitId == null ? false : true
            };

            await _context.PropertyOwners.AddAsync(propertyOwner);
            await _context.SaveChangesAsync();
        }

        public async Task<object> List(int? propertyId = null)
        {
            var query = _context.PropertyOwners.Where(x => x.IsActive.Value).Include(x => x.Owner).Include(x => x.Property).Include(x => x.Unit).AsQueryable();

            if (propertyId != null)
                query = query.Where(x => x.PropertyId == propertyId.Value);

            var data = await query.Select(x => new
            {
                x.Id,
                x.IsUnitOwnership,
                x.PropertyId,
                x.UnitId,
                ContractSigningDate = x.ContractSigningDate.ToShortPersianDateString(false),
                x.Description,
                x.DisSatisfactionLevelReason,
                x.HasDesireToIntroduce,
                x.HasIntroductionLeadsToPurchase,
                Owner = new
                {
                    x.Owner.Id,
                    x.Owner.PhoneNumber,
                    x.Owner.FirstName,
                    x.Owner.LastName,
                },
                Property = new
                {
                    x.Property.Id,
                    x.Property.Title,
                    x.Property.Address,
                    x.Property.FloorCount,
                    x.Property.Image,
                },
                Unit = new
                {
                    x.Unit.Id,
                    x.Unit.PropertyId,
                    x.Unit.Meterage,
                    x.Unit.Floor,
                },
                x.SatisfactionLevel
            }).ToListAsync();

            return data;
        }

        public async Task<object> Detail(int id)
        {
            var data = await _context.PropertyOwners.Where(x => x.IsActive.Value && x.Id == id).Include(x => x.Owner).Include(x => x.Property).Include(x => x.Unit).Select(x => new
            {
                x.Id,
                x.IsUnitOwnership,
                x.PropertyId,
                x.UnitId,
                ContractSigningDate = x.ContractSigningDate.ToShortPersianDateString(false),
                x.Description,
                x.DisSatisfactionLevelReason,
                x.HasDesireToIntroduce,
                x.HasIntroductionLeadsToPurchase,
                Owner = new
                {
                    x.Owner.Id,
                    x.Owner.PhoneNumber,
                    x.Owner.FirstName,
                    x.Owner.LastName,
                },
                Property = new
                {
                    x.Property.Id,
                    x.Property.Title,
                    x.Property.Address,
                    x.Property.FloorCount,
                    x.Property.Image,
                },
                Unit = new
                {
                    x.Unit.Id,
                    x.Unit.PropertyId,
                    x.Unit.Meterage,
                    x.Unit.Floor,
                },
                x.SatisfactionLevel
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task Edit(EditPropertyOwnerVM input)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime date = new DateTime(input.ContractSigningDate.Year, input.ContractSigningDate.Month, input.ContractSigningDate.Day, pc);

            var data = await _context.PropertyOwners.Where(x => x.IsActive.Value && x.Id == input.Id).FirstOrDefaultAsync();

            data.ContractSigningDate = date;
            data.Description = input.Description;
            data.DisSatisfactionLevelReason = input.DisSatisfactionLevelReason;
            data.HasDesireToIntroduce = input.HasDesireToIntroduce;
            data.HasIntroductionLeadsToPurchase = input.HasIntroductionLeadsToPurchase;
            data.SatisfactionLevel = input.SatisfactionLevel;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var owner = await _context.PropertyOwners.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == id);

            owner.IsActive = false;
            _context.Update(owner);
            await _context.SaveChangesAsync();
        }
    }
}
