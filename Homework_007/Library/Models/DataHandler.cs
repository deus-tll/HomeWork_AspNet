using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class DataHandler
    {
        private readonly ApplicationContext _context;
        public DataHandler(ApplicationContext db)
        {
            _context = db;
        }


        public async Task<List<User>> GetUsersAsync() => await _context.Users.AsNoTracking().ToListAsync();


        private async Task<List<Book>> GetBooksFromDbAsync() => await _context.Books.AsNoTracking().ToListAsync();


        public async Task<List<Book>> GetBooksAsync()
        {
            List<Book> books = await GetBooksFromDbAsync();

            if (!books.Any())
            {
                await _context.AddRangeAsync(Extension.CreateListOfDefaultBooks());
                await _context.SaveChangesAsync();
                books = await GetBooksFromDbAsync();
            }

            return books;
        }


        public async Task<Book?> GetBookAsync(int id) => await _context.Books.FindAsync(id);


        public async Task<List<Book>> GetFilteredBooksAsync(BookFilter filter)
        {
            List<Book> books = await GetBooksFromDbAsync();

            books = books
            .Where(b => (string.IsNullOrEmpty(filter.Title) || b.Title.ToLower().Contains(filter.Title.ToLower())) &&
                        (string.IsNullOrEmpty(filter.Author) || b.Author.ToLower().Contains(filter.Author.ToLower())) &&
                        (string.IsNullOrEmpty(filter.Publisher) || b.Publisher.ToLower().Contains(filter.Publisher.ToLower())) &&
                        (string.IsNullOrEmpty(filter.Genre) || b.Genre.ToLower().Contains(filter.Genre.ToLower())) &&
                        (filter.MinPageCount is null || b.PageCount >= filter.MinPageCount) &&
                        (filter.MaxPageCount is null || b.PageCount <= filter.MaxPageCount) &&
                        (filter.MinYear is null || b.Year >= filter.MinYear) &&
                        (filter.MaxYear is null || b.Year <= filter.MaxYear))
                        .ToList();

            books = filter.SortBy switch
            {
                "Title" => filter.IsSortAscending ? books.OrderBy(b => b.Title).ToList() : books.OrderByDescending(b => b.Title).ToList(),
                "Author" => filter.IsSortAscending ? books.OrderBy(b => b.Author).ToList() : books.OrderByDescending(b => b.Author).ToList(),
                "Publisher" => filter.IsSortAscending ? books.OrderBy(b => b.Publisher).ToList() : books.OrderByDescending(b => b.Publisher).ToList(),
                _ => books
            };

            return books;
        }


        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
