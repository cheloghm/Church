using Church.Models;

namespace Church.Helpers
{
    public class PaginationHelper
    {
        public static PagedResult<T> CreatePagedResult<T>(IQueryable<T> query, int page, int pageSize)
        {
            var skipAmount = pageSize * (page - 1);

            var projection = query
                .Skip(skipAmount)
                .Take(pageSize);

            var totalNumberOfRecords = query.Count();
            var results = projection.ToList();
            var totalNumberOfPages = (int)Math.Ceiling((double)totalNumberOfRecords / pageSize);

            return new PagedResult<T>
            {
                Results = results,
                CurrentPage = page,
                PageSize = pageSize,
                TotalNumberOfPages = totalNumberOfPages,
                TotalNumberOfRecords = totalNumberOfRecords
            };
        }
    }
}
