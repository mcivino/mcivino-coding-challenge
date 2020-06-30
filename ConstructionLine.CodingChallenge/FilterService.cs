using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionLine.CodingChallenge
{
    public class FilterService
    {
        private const int BatchSize = 100000;
        
        public List<Shirt> ApplyFilter(List<Shirt> shirts, SearchOptions options)
        {
            var sizeIds = options.Sizes.Select(s => s.Id).ToList();
            var colorIds = options.Colors.Select(c => c.Id).ToList();

            var shirtBatches = shirts.Split(BatchSize);

            var filterTasks = shirtBatches.Select(batch => GetShirtBatchResult(batch, colorIds, sizeIds)).ToList();
            
            Task.WhenAll(filterTasks);

            return filterTasks.SelectMany(t => t.Result).ToList();
        }

        private static Task<IEnumerable<Shirt>> GetShirtBatchResult(List<Shirt> batch, List<Guid> colorIds, List<Guid> sizeIds)
        {
            return Task.Factory.StartNew(() => batch.Where(shirt => (sizeIds.Contains(shirt.Size.Id)) && colorIds.Contains(shirt.Color.Id)));
        }
    }
}
