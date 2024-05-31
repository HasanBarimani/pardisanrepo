using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModel.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Services
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly ApplicationDbContext _context;

        public FeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(CreateFeatureVM input)
        {
            var feature = new Feature
            {
                Text = input.Text,
            };
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
            var list = new List<FeatureOption>();

            foreach (var item in input.Options)
            {
                list.Add(new FeatureOption
                {
                    Feature = feature,
                    FeatureId = feature.Id,
                    Text = item.Text,
                });
            }
            await _context.FeatureOptions.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var feature = await _context.Features.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == id);

            feature.IsActive = false;
            _context.Update(feature);
            await _context.SaveChangesAsync();
        }

        public async Task<object> Detail(int id)
        {

            var feature = await _context.Features
               .Where(x => x.IsActive.Value && x.Id == id).Include(x => x.Options).Select(x => new
               {
                   x.Id,
                   x.Text,
                   options = x.Options.Select(y => new
                   {
                       y.Id,
                       y.Text,
                       y.FeatureId
                   })
               }).FirstOrDefaultAsync();

            return feature;
        }

        public async Task<bool> DoesItExist(int id)
        {
            var result = await _context.Features.AnyAsync(x => x.IsActive.Value && x.Id == id);
            return result;
        }

        public async Task Edit(EditFeatureVM input)
        {
            var feature = await _context.Features.FirstOrDefaultAsync(x => x.Id == input.Id);

            feature.Text = input.Text;

            if (input.Options != null)
            {
                var options = _context.FeatureOptions.Where(x => x.FeatureId == input.Id).ToList();

                var newOptions = input.Options.Where(x => x.Id == 0).ToList();

                foreach (var item in newOptions)
                {
                    item.Feature = feature;
                    item.FeatureId = feature.Id;
                }
                await _context.FeatureOptions.AddRangeAsync(newOptions);

                var deletedOptions = new List<FeatureOption>();
                foreach (var item in options)
                {
                    if (input.Options.Where(x => x.Id == item.Id).Any())
                    {
                        var newOption = input.Options.FirstOrDefault(x => x.Id == item.Id);
                        item.Text = newOption.Text;
                        _context.Update(item);
                    }
                    else
                    {
                        deletedOptions.Add(item);
                    }
                }

                _context.FeatureOptions.RemoveRange(deletedOptions);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<object> GetAll()
        {
            var data = await _context.Features.Where(x=>x.IsActive.Value).Select(x => new
            {
                x.Id,
                x.Text,
                Options = x.Options.Select(x => new
                {
                    x.Id,
                    x.Text,
                    x.FeatureId
                })
            }).ToListAsync();

            return data;
        }
    }
}
