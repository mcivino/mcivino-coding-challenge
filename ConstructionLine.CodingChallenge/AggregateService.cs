using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class AggregateService
    {
        public List<SizeCount> GetSizeCounts(List<Shirt> shirts, List<Color> options)
        {
            return (from size in Size.All 
                    let count = shirts.Count(s => s.Size.Id == size.Id && (!options.Any() || options.Select(c => c.Id).Contains(s.Color.Id))) 
                    select new SizeCount {Size = size, Count = count}).ToList();
        }

        public List<ColorCount> GetColorCounts(List<Shirt> shirts, List<Size> options)
        {
            return (from color in Color.All
                    let count = shirts.Count(s => s.Color.Id == color.Id && (!options.Any() || options.Select(c => c.Id).Contains(s.Size.Id)))
                    select new ColorCount { Color = color, Count = count }).ToList();
        }
    }
}
