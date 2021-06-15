using BooksAPI.Models;
using BooksAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookservice;
        public BooksController(IBookService bookservice)
        {
            this.bookservice = bookservice;
        }
        [HttpGet]
        public string GetBooks()
        {
            return JsonConvert.SerializeObject(bookservice.GetAll()); ;
        }
        [HttpGet("{id}")]
        public string GetBook(int id)
        {
            return JsonConvert.SerializeObject(bookservice.Get(id)); ;
        }
        [HttpPost]
        public int CreateBook(Book book)
        {
            return bookservice.Create(book);
        }
        [HttpDelete]
        public int DeleteBook(int id)
        {
            return bookservice.Delete(id);
        }
        [HttpPatch("{id}")]
        public int UpdateBook(Book book,int id)
        {
            return bookservice.Update(book,id);
        }
    }
}
