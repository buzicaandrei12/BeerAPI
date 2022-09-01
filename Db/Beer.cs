using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeerAPI.Db
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [DefaultValue(0)]
        public double Rating { get; set; }
        public int RatingsNumber { get; set; }
        public int OneStar { get; set; }
        public int TwoStar { get; set; }
        public int ThreeStar { get; set; }
        public int FourStar { get; set; }
        public int FiveStar { get; set; }

        [Range(1, 5)]
        public int Stars
        {
            set
            {
                switch (value)
                {
                    case 1:
                        OneStar++;
                        RatingsNumber++;
                        break;
                    case 2:
                        TwoStar++;
                        RatingsNumber++;
                        break;
                    case 3:
                        ThreeStar++;
                        RatingsNumber++;
                        break;
                    case 4:
                        RatingsNumber++;
                        FourStar++;
                        break;
                    case 5:
                        RatingsNumber++;
                        FiveStar++;
                        break;
                }
            }
        }
    }
}
