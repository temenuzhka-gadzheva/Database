namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Globalization;
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
            // Console.WriteLine(GetBooksByCategory(db, "horror mystery drama"));
            //  Console.WriteLine(GetBooksReleasedBefore(db, "12-04-1992"));
            //  Console.WriteLine(GetAuthorNamesEndingIn(db, "dy"));
            //Console.WriteLine(GetBookTitlesContaining(db, "sK"));
            // Console.WriteLine(GetBooksByAuthor(db, "R"));
            //Console.WriteLine(CountBooks(db, 40));
            //Console.WriteLine(CountCopiesByAuthor(db));
            // Console.WriteLine(GetTotalProfitByCategory(db));
            // Console.WriteLine(GetMostRecentBooks(db));
            // IncreasePrices(db);
            Console.WriteLine(RemoveBooks(db));
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
            var castDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var books = context.Books
            .Where(x => x.ReleaseDate.Value < castDate)
            .Select(x => new
            {
                x.Title,
                x.EditionType,
                x.Price,
                x.ReleaseDate
            })
            .OrderByDescending(x => x.ReleaseDate)
            .ToList();

            return string.Join(Environment.NewLine, books.Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:F2}"));
        }

        //  author search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                  .Where(x => x.FirstName.EndsWith(input))
                  .Select(x => new { FullName = x.FirstName + " " + x.LastName })
                  .OrderBy(x => x.FullName)
                  .ToList();
            return string.Join(Environment.NewLine, authors.Select(x => x.FullName));
        }

        // book search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(x => x.Title.ToUpper().Contains(input.ToUpper()))
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToList();
            return string.Join(Environment.NewLine, books);
        }

        // search book by author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                 .Where(x => x.Author.LastName.ToUpper().StartsWith(input.ToUpper()))
                 .Select(x => new
                 {
                     x.BookId,
                     x.Title,
                     x.Author.FirstName,
                     x.Author.LastName
                 })
                 .OrderBy(x => x.BookId)
                 .ToList();

            return string.Join(Environment.NewLine, books.Select(x => $"{x.Title} ({x.FirstName} {x.LastName})"));
        }

        // count books
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
                .Where(x => x.Title.Length > lengthCheck)
                .Select(x => new { x.Title })
                .ToList();

            return books.Count();

        }

        // total book copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var totalBooksCopies = context.Authors
                 .Select(x => new
                 {
                     x.FirstName,
                     x.LastName,
                     totalBooksCount = x.Books.Sum(c => c.Copies)
                 })
                 .OrderByDescending(x => x.totalBooksCount)
                 .ToList();
            return string.Join(Environment.NewLine, totalBooksCopies.Select(book => $"{book.FirstName} {book.LastName} - {book.totalBooksCount}"));
        }

        // profit by category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var books = context.Categories
               .Select(x => new
               {
                   x.Name,
                   TotalProfit = x.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
               })
               .OrderByDescending(x => x.TotalProfit)
               .ThenBy(x => x.Name)
               .ToList();

            return string.Join(Environment.NewLine, books.Select(x => $"{x.Name} ${x.TotalProfit:F2}"));

        }

        // most recent books 
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categoryBooks = context.Categories
                 .Select(x => new
                 {
                     x.Name,
                     Books = x.CategoryBooks.Select(b => new
                     {
                         b.Book.Title,
                         b.Book.ReleaseDate.Value
                     })
                 .OrderByDescending(b => b.Value)
                 .Take(3)
                 .ToList()
                 })
                 .OrderBy(x => x.Name)
                 .ToList();

            var sb = new StringBuilder();

            foreach (var category in categoryBooks)
            {
                sb.AppendLine($"--{category.Name}");
                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.Value.Year})");
                }
            }
            return sb.ToString().Trim();
        }

        // increase prices
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.ReleaseDate.Value.Year < 2010)
                .ToList();
            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        // remove books
        public static int RemoveBooks(BookShopContext context)
        {
            var booksLessThan4200Copies = context.Books
                 .Where(x => x.Copies < 4200)
                 .ToList();

            context.Books.RemoveRange(booksLessThan4200Copies);
            context.SaveChanges();

            return booksLessThan4200Copies.Count();
        }


    }
}
