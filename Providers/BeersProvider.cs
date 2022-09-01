using AutoMapper;
using BeerAPI.Db;
using BeerAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerAPI.Providers
{
    public class BeersProvider : IBeersProvider
    {
        private readonly BeerDbContext _dbContext;
        private readonly IMapper _mapper;

        public BeersProvider(BeerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task Delete(int id)
        {
            var beer = await _dbContext.Beers.FindAsync(id);
            _dbContext.Beers.Remove(beer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Models.Beer> Rate(int id, int stars)
        {
            var beer = await _dbContext.Beers.FindAsync(id);
            beer.Stars = stars;
            var value = (double)(1 * beer.OneStar + 2 * beer.TwoStar + 3 * beer.ThreeStar + 4 * beer.FourStar + 5 * beer.FiveStar) / beer.RatingsNumber;
            beer.Rating = Math.Round(value, 2, MidpointRounding.AwayFromZero);

            _dbContext.Entry(_mapper.Map<Beer>(beer)).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Models.Beer>(beer);
        }

        public async Task<(bool Success, Models.Beer Beers, string ErrorMessage)> Add(Models.Beer beer)
        {
            var exists = _dbContext.Beers.Any(b => b.Name == beer.Name && b.Type == beer.Type);

            if (exists)
            {
                return (false, beer, "Element already exists");
            }

            _dbContext.Beers.Add(_mapper.Map<Models.Beer, Beer>(beer));
            await _dbContext.SaveChangesAsync();
            return (true, beer, null);
        }

        public async Task<(bool Success, IEnumerable<Models.Beer> Beers, string ErrorMessage)> GetBeersAsync(string name)
        {
            var beers = await _dbContext.Beers.Where(b => b.Name.Contains(name)).ToListAsync();
            if (beers != null)
            {
                var result = _mapper.Map<IEnumerable<Models.Beer>>(beers);
                return (true, result, null);
            }
            return (false, null, "No beers found");
        }

        public async Task<(bool Success, IEnumerable<Models.Beer> Beers, string ErrorMessage)> GetBeersAsync()
        {
            var beers = await _dbContext.Beers.ToListAsync();
            if (beers != null)
            {
                return (true, _mapper.Map<IEnumerable<Models.Beer>>(beers), null);
            }
            return (false, null, "No beers found");            
        }
    }
}
