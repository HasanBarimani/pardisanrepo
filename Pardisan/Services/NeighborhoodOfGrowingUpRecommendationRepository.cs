using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Services
{
    public class NeighborhoodOfGrowingUpRecommendationRepository : INeighborhoodOfGrowingUpRecommendationRepository
    {
        private readonly ApplicationDbContext _context;

        public NeighborhoodOfGrowingUpRecommendationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetAll()
        {
            var data = await _context.NeighborhoodOfGrowingUpRecommendations.Where(x => x.IsActive.Value).Select(x => new
            {
                x.Title
            }).ToListAsync();

            return data;
        }
    }
}
