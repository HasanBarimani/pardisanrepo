using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels;
using Pardisan.ViewModels.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore;
using Pardisan.Models;
using System.Globalization;
using Pardisan.ViewModels.API.Owner;
using Pardisan.Exceptions;

namespace Pardisan.Services
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDbContext _context;
        public OwnerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetAll()
        {
            var data = await _context.Owners.Where(x => x.IsActive.Value).Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.NeighborhoodOfGrowingUp,
                x.PhoneNumber,
                x.Job,
                x.IncomeBase,
                BirthDate = x.BirthDate.ToShortPersianDateString(false),
                x.Instagram,
                x.Telegram,
                x.Whatsapp,
                CreatedAt = x.CreatedAt.ToPersianDateTextify(false)
            }).ToListAsync();

            return data;
        }
        public async Task<int> Create(CreateOwnerVM input)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime date = new DateTime(input.BirthDate.Year, input.BirthDate.Month, input.BirthDate.Day, pc);

            var owner = new Owner
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                IncomeBase = input.IncomeBase,
                Instagram = input.Instagram,
                Job = input.Job,
                NeighborhoodOfGrowingUp = input.NeighborhoodOfGrowingUp,
                PhoneNumber = input.PhoneNumber,
                Telegram = input.Telegram,
                Whatsapp = input.Whatsapp,
                BirthDate = date
            };

            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();

            if (!_context.NeighborhoodOfGrowingUpRecommendations.Where(x => x.Title == owner.NeighborhoodOfGrowingUp).Any())
            {
                var neighborhoodOfGrowingUpRecommendation = new NeighborhoodOfGrowingUpRecommendation
                {
                    Title = owner.NeighborhoodOfGrowingUp
                };
                await _context.NeighborhoodOfGrowingUpRecommendations.AddAsync(neighborhoodOfGrowingUpRecommendation);
                await _context.SaveChangesAsync();
            }

            return owner.Id;
        }

        public async Task<object> Detail(int id)
        {
            var owner = await _context.Owners
                .Where(x => x.IsActive.Value && x.Id == id).Select(x => new
                {
                    x.Id,
                    x.FirstName,
                    x.LastName,
                    x.NeighborhoodOfGrowingUp,
                    x.PhoneNumber,
                    x.Job,
                    x.IncomeBase,
                    BirthDate = x.BirthDate.ToShortPersianDateString(false),
                    x.Instagram,
                    x.Telegram,
                    x.Whatsapp,
                    CreatedAt = x.CreatedAt.ToPersianDateTextify(false)

                }).FirstOrDefaultAsync();

            return owner;
        }

        public async Task<object> GetOwnerProperties(int id)
        {
            var data = await _context.PropertyOwners.Where(x => x.IsActive.Value && x.OwnerId == id).Include(x => x.Property).Include(x => x.Unit).Select(x => new
            {
                x.Id,
                x.IsUnitOwnership,
                x.PropertyId,
                x.UnitId,
                x.ContractSigningDate,
                x.Description,
                x.DisSatisfactionLevelReason,
                x.HasDesireToIntroduce,
                x.HasIntroductionLeadsToPurchase,
                x.Property,
                x.Unit,
                x.SatisfactionLevel
            }).ToListAsync();

            //var propertyOwnership = data.Where(y => !y.IsUnitOwnership).Select(y => y.PropertyId).ToList();
            //var unitOwnership = data.Where(y => y.IsUnitOwnership).Select(y => y.UnitId).ToList();

            //var properties = _context.Properties.Where(x => x.IsActive.Value && propertyOwnership.Contains(x.Id)).ToList();

            //var units = _context.Units.Where(x => x.IsActive.Value &&  unitOwnership.Contains(x.Id)).ToList();

            return data;
        }
        public async Task<bool> DoesItExist(int id)
        {
            var result = await _context.Owners.AnyAsync(x => x.IsActive.Value && x.Id == id);
            return result;
        }

        public async Task Edit(EditOwnerVM input)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == input.Id);

            PersianCalendar pc = new PersianCalendar();
            DateTime date = new DateTime(input.BirthDate.Year, input.BirthDate.Month, input.BirthDate.Day, pc);

            owner.BirthDate = date;
            owner.FirstName = input.FirstName;
            owner.LastName = input.LastName;
            owner.Instagram = input.Instagram;
            owner.IncomeBase = input.IncomeBase;
            owner.Job = input.Job;
            owner.NeighborhoodOfGrowingUp = input.NeighborhoodOfGrowingUp;
            owner.Telegram = input.Telegram;
            owner.Whatsapp = input.Whatsapp;

            _context.Update(owner);
            await _context.SaveChangesAsync();

            if (!_context.NeighborhoodOfGrowingUpRecommendations.Where(x => x.Title == owner.NeighborhoodOfGrowingUp).Any())
            {
                var neighborhoodOfGrowingUpRecommendation = new NeighborhoodOfGrowingUpRecommendation
                {
                    Title = owner.NeighborhoodOfGrowingUp
                };
                await _context.NeighborhoodOfGrowingUpRecommendations.AddAsync(neighborhoodOfGrowingUpRecommendation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == id);

            owner.IsActive = false;
            _context.Update(owner);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsPhoneNumberAlreadyInSystem(string phoneNumber)
        {
            var result = await _context.Owners.Where(x => x.PhoneNumber == phoneNumber).AnyAsync();
            return result;
        }

        public async Task<object> GetForDashboard()
        {
            var data = await _context.Owners.Where(x => x.IsActive.Value).ToListAsync();
            return new
            {
                LastFive = data.OrderByDescending(x => x.Id),
                Count = data.Count
            };
        }
    }
}
