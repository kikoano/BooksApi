using BooksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Services
{
    public interface IBookService
    {
        DataTable GetAll();
        DataTable Get(int id);
        int Create(Book book);
        int Delete(int id);
        int Update(Book book, int id);
    }
    public class BookService : IBookService
    {
        private readonly IDatabase<Book> database;
        public BookService(IDatabase<Book> database)
        {
            this.database = database;
        }

        public DataTable GetAll()
        {
            return database.GetAll();
        }


        public DataTable Get(int id)
        {
            return database.Get(id);
        }

        int IBookService.Create(Book book)
        {
            return database.Create(book);
        }

        public int Delete(int id)
        {
            return database.Delete(id);
        }

        public int Update(Book book, int id)
        {
            return database.Update(book, id);
        }
    }
}
