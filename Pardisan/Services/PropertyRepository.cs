using Pardisan.Data;
using Pardisan.Interface;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels.API.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore;
using Pardisan.Migrations;

namespace Pardisan.Services
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploaderService _uploaderService;

        public PropertyRepository(ApplicationDbContext context, IUploaderService uploaderService)
        {
            _context = context;
            _uploaderService = uploaderService;
        }

        public async Task<int> Create(CreatePropertyVM input)
        {
            var path = await _uploaderService.SimpleUpload(input.Image, "/Img/Property/");

            var property = new Property
            {
                Address = input.Address,
                Description = input.Description,
                //FloorCount = input.FloorCount,
                ProjectSupervisor = input.ProjectSupervisor,
                Title = input.Title,
                Image = path
            };
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            return property.Id;
            //foreach (var item in input.Units)
            //{
            //    item.PropertyId = property.Id;
            //}
            //_context.Units.AddRange(input.Units);
            //await _context.SaveChangesAsync();

            //var allFeatures = new List<PropertyFeatures>();
            //if (input.Features != null)
            //{
            //    foreach (var item in input.Features)
            //    {
            //        item.PropertyId = property.Id;
            //        if (item.Value != "0" && item.Value != null)
            //        {
            //            allFeatures.Add(item);
            //        }
            //    }
            //    await _context.PropertyFeatures.AddRangeAsync(allFeatures);
            //    await _context.SaveChangesAsync();
            //}
        }

        public async Task SaveFullProperty(FullPropertyVM input)
        {
            var property = await _context.Properties.SingleOrDefaultAsync(x => x.Id == input.Id);
            property.FloorCount = input.FloorsCount.ToString();

            var oldUnits = _context.Units.Where(x => x.PropertyId == property.Id);

            foreach (var item in oldUnits)
            {
                item.IsActive = false;
                _context.Update(item);
            }
            await _context.SaveChangesAsync();

            foreach (var item in input.Floors)
            {
                foreach (var unit in item.Units)
                {
                    var unitToDb = new Unit
                    {
                        Floor = unit.FloorNumber,
                        Meterage = unit.Meterage,
                        Number = unit.Number,
                        PropertyId = input.Id,

                        MergedUnitId = unit.MergedUnitId,
                    };

                    await _context.Units.AddAsync(unitToDb);
                    await _context.SaveChangesAsync();
                    if (unit.OwnerId != null)
                    {
                        foreach (var i in unit.OwnerId)
                        {
                            var owners = new PropertyOwnersList
                            {
                                OwnersId = i,
                                UnitId = unitToDb.Id


                            };
                            await _context.PropertyOwnersList.AddAsync(owners);
                            await _context.SaveChangesAsync();
                        }
                    }
                    await _context.SaveChangesAsync();
                }
            }

            await _context.SaveChangesAsync();

        }
        public async Task<FullPropertyVM> GetFullProperty(int id)
        {
            var property = await _context.Properties.SingleOrDefaultAsync(x => x.Id == id);

            var vm = new FullPropertyVM
            {
                Id = id,
                FloorsCount = int.Parse(property.FloorCount),
            };

            var oldUnits = _context.Units.Where(x => x.PropertyId == property.Id && x.IsActive.Value);

            var list = new List<FloorVM>();
            for (int i = 0; i < vm.FloorsCount; i++)
            {
                var floor = new FloorVM
                {
                    FloorNumber = i,
                    Units = oldUnits.Where(x => x.Floor == i).Select(x => new UnitVM
                    {
                        FloorNumber = x.Floor,
                        Id = x.Id,
                        MergedUnitId = x.MergedUnitId,
                        Meterage = x.Meterage,
                        Number = x.Number,
                        OwnerId = _context.PropertyOwnersList.Where(y => y.UnitId == x.Id).Select(x=>x.OwnersId).ToList(),
                       
                    }).ToList(),
                };
                list.Add(floor);
            }

            vm.Floors = list;
            return vm;
        }
        public async Task<object> Detail(int id)
        {
            var data = await _context.Properties.Where(x => x.IsActive.Value && x.Id == id).Select(x => new
            {
                x.Id,
                x.Address,
                x.Description,
                x.FloorCount,
                x.Image,
                x.ProjectSupervisor,
                x.Title,
                CreatedAt = x.CreatedAt.ToPersianDateTextify(false),
                Units = x.Units.Select(y => new
                {
                    y.Id,
                    y.Floor,
                    y.Meterage,
                    y.PropertyId,
                    OwnerId = _context.PropertyOwnersList.Where(x => x.UnitId == y.Id).Select(y => y.OwnersId).ToList()
                }),

                Features = x.Features.Select(y => new
                {
                    y.Id,
                    y.PropertyId,
                    y.Key,
                    y.Value
                })
            }).FirstOrDefaultAsync();

            return data;
        }

        public async Task<bool> DoesItExist(int id)
        {
            var result = await _context.Properties.AnyAsync(x => x.IsActive.Value && x.Id == id);
            return result;
        }

        public async Task Delete(int id)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == id);

            property.IsActive = false;
            _context.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task<object> GetAll()
        {
            var data = await _context.Properties.Where(x => x.IsActive.Value).Select(x => new
            {
                x.Id,
                x.Address,
                x.Description,
                x.FloorCount,
                x.Image,
                x.ProjectSupervisor,
                x.Title,
                CreatedAt = x.CreatedAt.ToPersianDateTextify(false),
                Units = x.Units.Select(y => new
                {
                    y.Id,
                    y.Floor,
                    y.Meterage,
                    y.PropertyId
                }),
                Features = x.Features.Select(y => new
                {
                    y.Id,
                    y.PropertyId,
                    y.Key,
                    y.Value
                })
            }).ToListAsync();

            return data;
        }

        public async Task AddItemToGallery(AddToGalleryVM input)
        {
            var image = new Pardisan.Models.Image
            {
                Date = DateTime.Now,
                PropertyId = input.PropertyId,
                Url = await _uploaderService.SimpleUpload(input.Item, "/Img/Property/")
            };

            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }
        public async Task Edit(EditPropertyVM input)
        {
            var property = await _context.Properties.Include(x => x.Features).FirstOrDefaultAsync(x => x.IsActive.Value && x.Id == input.Id);

            property.Title = input.Title;
            property.Address = input.Address;
            property.Description = input.Description;
            property.FloorCount = input.FloorCount;
            property.ProjectSupervisor = input.ProjectSupervisor;

            if (input.Image != null)
            {
                var path = await _uploaderService.SimpleUpload(input.Image, "/Img/Property/");
                property.Image = path;
            }

            //if (input.Features != null)
            //{
            //    var features = property.Features;

            //    var newFeatures = input.Features.Where(x => x.Id == 0).ToList();

            //    foreach (var item in newFeatures)
            //    {
            //        item.Id = 0;
            //        item.PropertyId = property.Id;
            //    }
            //    await _context.PropertyFeatures.AddRangeAsync(newFeatures);
            //    await _context.SaveChangesAsync();

            //    var deletedFeatures = new List<PropertyFeatures>();
            //    foreach (var item in features)
            //    {
            //        if (input.Features.Where(x => x.Id == item.Id).Any())
            //        {
            //            var newFeature = input.Features.FirstOrDefault(x => x.Id == item.Id);
            //            item.Value = newFeature.Value;
            //            _context.PropertyFeatures.Update(item);
            //        }
            //        else
            //        {
            //            deletedFeatures.Add(item);
            //        }
            //    }

            //    _context.PropertyFeatures.RemoveRange(deletedFeatures);
            //}

            await _context.SaveChangesAsync();
            //if (input.Features != null)
            //{
            //    var newFeatures = new List<PropertyFeatures>();
            //    var deletedFeatures = new List<PropertyFeatures>();
            //    var features = property.Features;
            //    foreach (var item in input.Features)
            //    {
            //        item.PropertyId = property.Id;
            //        if (item.Value != "0" && item.Value != null || !string.IsNullOrWhiteSpace(item.Value))
            //        {
            //            var feature = features.FirstOrDefault(x => x.Key == item.Key);
            //            if (feature == null)
            //            {
            //                newFeatures.Add(item);
            //            }
            //            else
            //            {
            //                feature.Value = item.Value;
            //                _context.Update(feature);
            //            }
            //        }
            //        else
            //        {
            //            var feature = features.FirstOrDefault(x => x.Key == item.Key);
            //            if (feature != null)
            //            {
            //                deletedFeatures.Add(feature);
            //            }
            //        }
            //    }
            //    await _context.PropertyFeatures.AddRangeAsync(newFeatures);
            //    _context.PropertyFeatures.RemoveRange(deletedFeatures);
            //    await _context.SaveChangesAsync();
            //}
        }

        public async Task<object> GetForDashboard()
        {
            var data = await _context.Properties.Where(x => x.IsActive.Value).ToListAsync();
            return new
            {
                LastFive = data.OrderByDescending(x => x.Id),
                Count = data.Count
            };
        }

        public async Task<object> Images(int id)
        {
            var images = await _context.Images.Where(x => x.PropertyId == id).Select(x => new
            {
                x.Id,
                x.Url
            }).ToListAsync();
            return images;
        }

        public async Task RemoveImageFromGallery(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
