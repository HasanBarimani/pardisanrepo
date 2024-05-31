using Pardisan.Data;
using Pardisan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Services
{
    public class FeatureOptionRepository : IFeatureOptionRepository
    {
        private readonly ApplicationDbContext _context;

        public FeatureOptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}
