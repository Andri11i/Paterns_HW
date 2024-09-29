using Paterns_HW.Classes;
using Paterns_HW.Data;


class Program
{
    static void Main()
    {
        using (var context = new BooksContext())
        {
            var unitOfWork = new UnitOfWork(context);
            var libraryService = new LibraryService(unitOfWork);

            while (true)
            {
                Console.WriteLine("1. Add book \n2. Update book \n3. Delete book\n4. Show books\n0. Exit");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Enter the book's name: ");
                        var title = Console.ReadLine();
                        Console.Write("Enter the author ID: ");
                        var authorId = int.Parse(Console.ReadLine());
                        Console.Write("Enter the genre ID: ");
                        var genreId = int.Parse(Console.ReadLine());
                        Console.Write("Enter the publication year: ");
                        var year = int.Parse(Console.ReadLine());

                        libraryService.AddBook(title, authorId, genreId, year);
                        break;

                    case "2":
                        Console.Write("Enter the book's id: ");
                        var id = int.Parse(Console.ReadLine());
                        Console.Write("Enter the new book's name: ");
                        var newTitle = Console.ReadLine();
                        Console.Write("Enter the new author ID: ");
                        var newAuthorId = int.Parse(Console.ReadLine());
                        Console.Write("Enter the new genre ID: ");
                        var newGenreId = int.Parse(Console.ReadLine());
                        Console.Write("Enter the new publication year: ");
                        var newYear = int.Parse(Console.ReadLine());

                        libraryService.UpdateBook(id, newTitle, newAuthorId, newGenreId, newYear);
                        break;

                    case "3":
                        Console.Write("Enter the ID of a book to delete: ");
                        var deleteId = int.Parse(Console.ReadLine());
                        libraryService.DeleteBook(deleteId);
                        break;

                    case "4":
                        Console.Write("Filter with genre (optional, enter genre's name): ");
                        var genreFilter = Console.ReadLine();
                        Console.Write("Filter with author (optional, enter author's name): ");
                        var authorFilter = Console.ReadLine();




                        var books = libraryService.GetBooks(genreFilter, authorFilter);
                        foreach (var book in books)
                        {
                            var author = unitOfWork.Authors.GetById(book.AuthorId);
                            var genre = unitOfWork.Genres.GetById(book.GenreId);
                            Console.WriteLine($"ID: {book.Id}, Name: {book.Title}, Author: {author.Name}, Genre: {genre.Name}, Year: {book.PublishedYear}");
                        }
                        break;

                    case "0":
                        return;
                }
            }
        }
    }
}
