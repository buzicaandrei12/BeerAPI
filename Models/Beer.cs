using System.ComponentModel.DataAnnotations;

namespace BeerAPI.Models
{
    public class Beer
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double? Rating { get; set; }
    }
}
