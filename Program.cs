using System;
using System.Linq;

class Author
{
    public string name { get; set; }
    public int birth_year { get; set; }

    public Author(string name, int birth_year)
    {
        this.name = name;
        this.birth_year = birth_year;
    }

    public string GetInfo()
    {
        return $"Iм'я: {name}, рiк народження: {birth_year}";
    }
}

class Book
{
    public string title { get; set; }
    public Author author { get; }
    public int year { get; set; }
    public string annotation { get; set; }

    public Book(string title, Author author, int year, string annotation = "")
    {
        this.title = title;
        this.author = author;
        this.year = year;
        this.annotation = annotation;
    }

    public string GetInfo()
    {
        string info = $"Назва: {title}, Рік видання: {year}, Автор: {author.name}";
        if (IsNullOrEmpty(annotation))
            info += $"\n{annotation}";
        return info;
    }
}

class Library
{
    public string name { get; set; }
    private List<Book> books;

    public Library(string name)
    {
        this.name = name;
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        if (books.Any(b => b.title == book.title && b.author.name == book.author.name))
        {
            Console.WriteLine("Ця книга вже є у бібліотеці!");
            return;
        }

        books.Add(book);
        Console.WriteLine($"Додано книгу: {book.title}");
    }

    public void RemoveBook(Book book)
    {
        if (books.Remove(book))
        {
            Console.WriteLine($"Книга {book.title} видалена.");
        }
        else
        {
            Console.WriteLine("Книгу не знайдено у бібліотеці.");
        }
    }

    public List<Book> FindBooksByYear(int year)
    {
        return books.Where(book => book.year == year).ToList();
    }

    public List<Book> FindBooksByKeyword(string keyword)
    {
        return books.Where(book => book.title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void ListBooks()
    {
        foreach (var book in books)
        {
            Console.WriteLine(book.GetInfo());
        }
    }
}

class Reader
{
    public string Name { get; set; }
    private List<Book> borrowedBooks;

    public Reader(string name)
    {
        Name = name;
        borrowedBooks = new List<Book>();
    }

    public void BorrowBook(Book book)
    {
        borrowedBooks.Add(book);
        Console.WriteLine($"{Name} взяв книгу: {book.title}");
    }

    public void ReturnBook(Book book)
    {
        if (borrowedBooks.Remove(book))
        {
            Console.WriteLine($"{Name} повернув книгу: {book.title}");
        }
        else
        {
            Console.WriteLine($"{Name} не має цієї книги.");
        }
    }

    public void ListBorrowedBooks()
    {
        Console.WriteLine($"Книги, які {Name} орендував:");
        foreach (var book in borrowedBooks)
        {
            Console.WriteLine(book.GetInfo());
        }
    }
}

class Program
{
    static void Main()
    {
        Author author1 = new Author("Біллі Джин", 1944);
        Author author2 = new Author("Ямана Ядара", 1980);

        Book book1 = new Book("Джин", author1, 1991, "мрія про розпад");
        Book book2 = new Book("Матік", author2, 1999, "поле битви.");
        Book book3 = new Book("Багрян", author2, 1979);
        Book book4 = new Book("Багрян", author2, 1979);

        Library library = new Library("Місцева бібліотека");
        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);
        library.AddBook(book4);
        library.ListBooks();

        Console.WriteLine("\nПошук книг за роком 1991:");
        var foundBooks = library.FindBooksByYear(1991);
        foreach (var book in foundBooks)
            Console.WriteLine(book.GetInfo());

        Console.WriteLine("\nПошук книг за ключовим словом 'Мітік':");
        foundBooks = library.FindBooksByKeyword("Матік");
        foreach (var book in foundBooks)
            Console.WriteLine(book.GetInfo());

        Reader reader = new Reader("Кум");
        reader.BorrowBook(book1);
        reader.ListBorrowedBooks();
        reader.ReturnBook(book1);
        reader.ListBorrowedBooks();
    }
}
