using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Paterns_HW.Models;

namespace Paterns_HW.Data;

public partial class BooksContext : DbContext
{
    public BooksContext()
    {
    }

    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options)
    {
    }

    DbSet<Author> Authors { get; set; }
    DbSet<Book> Books { get; set; }
    DbSet<Genre> Genres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=TUF;Initial Catalog=Paterns_HW;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Library");
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
