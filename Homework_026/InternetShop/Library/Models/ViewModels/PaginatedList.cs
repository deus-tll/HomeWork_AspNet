using Microsoft.EntityFrameworkCore;

namespace Library.Models.ViewModels
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;


        private PaginatedList(List<T> items, int count, int pageIndex, int pageSize, string controller, string action)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Controller = controller;
            Action = action;

            Items = items;
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, string controller, string action)
        {
            int count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize, controller, action);
        }
    }
}
