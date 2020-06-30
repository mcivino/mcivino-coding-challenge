using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly AggregateService _aggregateService;
        private readonly FilterService _filterService;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.
            _aggregateService = new AggregateService();
            _filterService = new FilterService();
        }

        public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.
            if (options == null)
                throw new ArgumentNullException();

            var colorCountTask = Task.Factory.StartNew(() => _aggregateService.GetColorCounts(_shirts, options.Sizes));
            var sizeCountTask = Task.Factory.StartNew(() => _aggregateService.GetSizeCounts(_shirts, options.Colors));
            var filterTask = Task.Factory.StartNew(() => _filterService.ApplyFilter(_shirts, options));

            Task.WhenAll(colorCountTask, sizeCountTask, filterTask);

            return new SearchResults
            {
                ColorCounts = colorCountTask.Result,
                SizeCounts = sizeCountTask.Result,
                Shirts = filterTask.Result
            };
        }
    }
}