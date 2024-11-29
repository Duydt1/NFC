using Microsoft.EntityFrameworkCore;

namespace NFC.Data.Common
{
	public class NFCUtil
    {
		public static List<T> GetLatestItemsByNum<T>(List<T> items, Func<T, string> getNum, Func<T, DateTime> getDateTime)
		{
			return items
				.GroupBy(item => getNum(item))
				.Select(group => group.OrderByDescending(getDateTime).First())
				.ToList();
		}
		public class PaginatedList<T> : List<T>
        {
            public List<T> Items { get; set; }
            public int TotalItems { get; set; }
            public int PageIndex { get; private set; }
            public int PageSize { get; set; }
            public int TotalPages { get; private set; }
			
			public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                Items = items;
                TotalItems = count;
                AddRange(items);
            }

            public bool HasPreviousPage => PageIndex > 1;

            public bool HasNextPage => PageIndex < TotalPages;
            public int FirstItemIndex => (PageIndex - 1) * PageSize + 1;
            public int LastItemIndex => Math.Min(PageIndex * PageSize, TotalItems);
			
			public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
            {
                try
                {
                    var count = await source.CountAsync();
                    var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                    return new PaginatedList<T>(items, count, pageIndex, pageSize);
                }
                catch (Exception ex) 
                { 
                    throw new Exception() ; 
                }

            }
			
		}
	}
}
