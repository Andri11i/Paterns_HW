using Microsoft.EntityFrameworkCore;
using Paterns_HW.Models;

public interface IUnitOfWork : IDisposable
{
    IRepository<Book> Books { get; }
    IRepository<Author> Authors { get; }
    IRepository<Genre> Genres { get; }
    void Commit();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private Repository<Book> _books;
    private Repository<Author> _authors;
    private Repository<Genre> _genres;
    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public IRepository<Book> Books
    {
        get
        {
            return _books ?? (_books = new Repository<Book>(_context));
        }
    }
    public IRepository<Author> Authors
    {
        get
        {
            return _authors ?? (_authors = new Repository<Author>(_context));
        }
    }

    public IRepository<Genre> Genres
    {
        get
        {
            return _genres ?? (_genres = new Repository<Genre>(_context));
        }
    }


    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

