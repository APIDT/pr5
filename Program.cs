using System;
using System.Collections.Generic;
using System.Linq;

class Author
{
    public string Name { get; }
    public int BirthYear { get; }

    public Author(string name, int birthYear)
    {
        Name = name;
        BirthYear = birthYear;
    }

    public string GetInfo()
    {
        return $"Ім'я: {Name}, Рік народження: {BirthYear}";
    }
}

class Book
{
    public string Title { get; }
    public Author Author { get; }
    public int Year { get; }
    public string Annotation { get; }

    public Book(string title, Author author, int year, string annotation = "")
    {
        Title = title;
        Author = author;
        Year = year;
        Annotation = annotation;
    }

    public string GetInfo()
    {
        string info = $"Назва: {Title}, Рік видання: {Year}, Автор: {Author.Name}";
        if (!string.IsNullOrEmpty(Annotation))
            info += $"\n{Annotation}";
        return info;
    }
}

class Library
{
    public string Name { get; }
    private List<Book> Books;

    public Library(string name)
    {
        Name = name;
        Books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        if (Books.Any(b => b.Title == book.Title && b.Author.Name == book.Author.Name))
        {
            Console.WriteLine("Ця книга вже є у бібліотеці!");
            return;
        }

        Books.Add(book);
        Console.WriteLine($"Додано книгу: {book.Title}");
    }

    public void RemoveBook(Book book)
    {
        if (Books.Remove(book))
        {
            Console.WriteLine($"Книга \"{book.Title}\" видалена.");
        }
        else
        {
            Console.WriteLine("Книгу не знайдено у бібліотеці.");
        }
    }

    public void ListBooks()
    {
        if (Books.Count == 0)
        {
            Console.WriteLine("Бібліотека порожня.");
            return;
        }

        Console.WriteLine($"Книги в бібліотеці \"{Name}\":");
        foreach (var book in Books)
        {
            Console.WriteLine(book.GetInfo());
        }
    }

    public List<Book> FindBooksByAuthor(string authorName)
    {
        return Books.Where(book => book.Author.Name.Equals(authorName, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Book> FindBooksByYear(int year)
    {
        return Books.Where(book => book.Year == year).ToList();
    }
}

class Reader
{
    public string Name { get; }
    private List<Book> BorrowedBooks;

    public Reader(string name)
    {
        Name = name;
        BorrowedBooks = new List<Book>();
    }

    public void BorrowBook(Book book, Library library)
    {
        if (library.FindBooksByYear(book.Year).Contains(book))
        {
            BorrowedBooks.Add(book);
            Console.WriteLine($"{Name} взяв книгу: {book.Title}");
        }
        else
        {
            Console.WriteLine($"Книги \"{book.Title}\" немає у бібліотеці!");
        }
    }

    public void ReturnBook(Book book)
    {
        if (BorrowedBooks.Remove(book))
        {
            Console.WriteLine($"{Name} повернув книгу: {book.Title}");
        }
        else
        {
            Console.WriteLine($"{Name} не має цієї книги.");
        }
    }

    public void ListBorrowedBooks()
    {
        if (BorrowedBooks.Count == 0)
        {
            Console.WriteLine($"{Name} не має жодної книги.");
            return;
        }

        Console.WriteLine($"Книги, які {Name} орендував:");
        foreach (var book in BorrowedBooks)
        {
            Console.WriteLine(book.GetInfo());
        }
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library("Місцева бібліотека");

        Author author1 = new Author("Біллі Джин", 1944);
        Author author2 = new Author("Ямана Ядара", 1980);

        Book book1 = new Book("Джин", author1, 1991, "Мрія про розпад");
        Book book2 = new Book("Матік", author2, 1999, "Поле битви.");
        Book book3 = new Book("Багрян", author2, 1979);

        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);

        library.ListBooks();

        Console.WriteLine("\nПошук книг за автором Ямана Ядара:");
        var foundBooks = library.FindBooksByAuthor("Ямана Ядара");
        foreach (var book in foundBooks)
            Console.WriteLine(book.GetInfo());

        Reader reader = new Reader("Кум");
        reader.BorrowBook(book1, library);
        reader.ListBorrowedBooks();
        reader.ReturnBook(book1);
        reader.ListBorrowedBooks();
    }
}
