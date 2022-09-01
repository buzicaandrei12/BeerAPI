using BeerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerAPI.Interfaces
{
    public interface IBeersProvider
    {
        Task Delete(int id);
        Task<Beer> Rate(int id, int stars);
        Task<(bool Success, Beer Beers, string ErrorMessage)> Add(Beer beer);
        Task<(bool Success, IEnumerable<Beer> Beers, string ErrorMessage)> GetBeersAsync();
        Task<(bool Success, IEnumerable<Beer> Beers, string ErrorMessage)> GetBeersAsync(string name);
        
    }
}