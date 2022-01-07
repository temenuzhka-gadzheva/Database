namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
             var db = new BookShopContext();
            // DbInitializer.ResetDatabase(db);
            //Console.WriteLine(GetBooksByAgeRestriction(db, "miNor"));
            //Console.WriteLine(GetGoldenBooks(db));
            // Console.WriteLine(GetBooksByPrice(db));
            //  Console.WriteLine(GetBooksNotReleasedIn(db, 2000));
            Console.WriteLine(GetBooksByCategory(db, "horror mystery drama"));

        }

        // age restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(x => x.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToList();
           return string.Join(Environment.NewLine, books);
        }
        // golden books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Copies < 5000 && x.EditionType == EditionType.Gold)
                .Select(x => new
                {
                    x.BookId,
                    x.Title
                })
                .OrderBy(x => x.BookId)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(x => x.Title));
            
        }
        // books by price
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Price > 40)
                .Select(x => new
                {
                    x.Title,
                    x.Price
                })
                .OrderByDescending(x => x.Price)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(x =>
           $"{x.Title} - ${x.Price:F2}"));
         

        }
        // Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books.Where(x => x.ReleaseDate.HasValue
            && x.ReleaseDate.Value.Year != year)
                .Select(x => new { x.BookId, x.Title })
                .OrderBy(x => x.BookId)
                .ToList();
            return string.Join(Environment.NewLine, books.Select(x => x.Title));
               
        }

        // book titles by category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToUpper())
                .ToList();

            var books = context.Books.Where(x => x.BookCategories.Any(c => categories.Contains(c.Category.Name.ToUpper())))
            .Select(x => x.Title)
            .OrderBy(b => b)
            .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b));
        }
        // Released Before Date

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {

        }

    }
}
