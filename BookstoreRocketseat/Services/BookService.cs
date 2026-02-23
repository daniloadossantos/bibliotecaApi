using System.Collections.Generic;
using System.Linq;
using BookstoreRocketseat.Communications.Requests;
using BookstoreRocketseat.Communications.Responses;
using BookstoreRocketseat.Controllers;
using BookstoreRocketseat.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookstoreRocketseat.Services;

//Criar Livro

public class BookService
{
    //Biblioteca para termos de exemplo
    private static List<Book> library = new List<Book>

{
    new Book
    {
        Id = Guid.NewGuid(),
        Title = "Harry Potter and the Sorcerer's Stone",
        Author = "J.K.Rowling",
        Genre = "Ficção",
        Price = 29.9m,
        Stock = 20,
        CreatedAt = DateTime.Now
    },

    new Book
    {
        Id = Guid.NewGuid(),
        Title = "Harry Potter and the Chamber of Secrets",
        Author = "J.K.Rowling",
        Genre = "Ficção",
        Price = 49.9m,
        Stock = 23,
        CreatedAt = DateTime.Now
    },

    new Book
    {
        Id = Guid.NewGuid(),
        Title = "Harry Potter and the Prisoner of Azkaban",
        Author = "J.K.Rowling",
        Genre = "Ficção",
        Price = 59.9m,
        Stock = 17,
        CreatedAt = DateTime.Now
    },

    new Book
    {
        Id = Guid.NewGuid(),
        Title = "Harry Potter and the Goblet of Fire",
        Author = "J.K.Rowling",
        Genre = "Ficção",
        Price = 69.9m,
        Stock = 19,
        CreatedAt = DateTime.Now
    },

    new Book
    {
        Id =  Guid.NewGuid(),
        Title =  "Harry Potter and the Order of the Phoenix",
        Author =  "J.K.Rowling",
        Genre =  "Ficção",
        Price =  99.9m,
        Stock = 5,
        CreatedAt = DateTime.Now
    },

    new Book
    {
        Id = Guid.NewGuid(),
        Title = "Harry Potter and the Half-Blood Prince",
        Author = "J.K.Rowling",
        Genre = "Ficção",
        Price = 69.9m,
        Stock = 47,
        CreatedAt = DateTime.Now
    },

    new Book
    {
        Id = Guid.NewGuid(),
        Title = "Harry Potter and the Deathly Hallows",
        Author = "J.K.Rowling",
        Genre = "Ficção",
        Price = 99.9m,
        Stock = 100,
        CreatedAt = DateTime.Now
    },

    new Book
    {
        Id = Guid.NewGuid(),
        Title = "Harry Potter and the Cursed Child",
        Author = "J.K.Rowling",
        Genre = "Ficção",
        Price = 109.9m,
        Stock = 3,
        CreatedAt = DateTime.Now
    }
};

    //CRUD
    //CREATE
    public ResponseBookJson Create(RequestNewBookJson request)
    {
        //1. Verificação de duplicação
        foreach (Book book in library)
        {
            if (book.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase) && book.Author.Equals(request.Author, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
        }

        //2. Processo de criação do livro pós-validação
        Book bookCreated = new Book();

        bookCreated.Id = Guid.NewGuid();
        bookCreated.Title = request.Title;
        bookCreated.Author = request.Author;
        bookCreated.Genre = request.Genre;
        bookCreated.Price = request.Price;
        bookCreated.Stock = request.Stock;
        bookCreated.CreatedAt = DateTime.Now;

        library.Add(bookCreated);

        //3. Converte para Response e retornar para Controller
        ResponseBookJson response = new ResponseBookJson()        
        {
            Id = bookCreated.Id,
            Title = bookCreated.Title,
            Author = bookCreated.Author,
            Genre = bookCreated.Genre,
            Price = bookCreated.Price,
            Stock = bookCreated.Stock,
            CreatedAt = bookCreated.CreatedAt
        };

        return (response);
    }

    //READ
    public List<ResponseBookJson> GetAllBooks()
    {
        List<ResponseBookJson> allBooksArray = new List<ResponseBookJson>();

        foreach (Book book in library) 
        {
            ResponseBookJson response = new ResponseBookJson();

            response.Id = book.Id;
            response.Title = book.Title;
            response.Author = book.Author;
            response.Genre = book.Genre;
            response.Price = book.Price;
            response.Stock = book.Stock;
            response.CreatedAt = book.CreatedAt;
            response.UpdatedAt = book.UpdatedAt;

            allBooksArray.Add(response);
        };

        return allBooksArray;

    }

    public ResponseBookJson? GetBookById(Guid id)
    {
        ResponseBookJson bookFound = new ResponseBookJson();

        foreach (Book book in library)
        {
            if (id == book.Id) {
                bookFound.Id = book.Id;
                bookFound.Title = book.Title;
                bookFound.Author = book.Author;
                bookFound.Genre = book.Genre;
                bookFound.Price = book.Price;
                bookFound.Stock = book.Stock;
                bookFound.CreatedAt = book.CreatedAt;
                bookFound.UpdatedAt = book.UpdatedAt;
                return (bookFound); 
            }
        }        
        return null;
    }
    
    //UPDATE
    public Book? UpdateBook(Guid id, RequestUpdateBookJson request)
    {
        foreach (Book book in library)
        {
            if (id == book.Id)
            {

                if (!string.IsNullOrEmpty(request.Title))
                {
                    book.Title = request.Title;
                }
                if (!string.IsNullOrEmpty(request.Author))
                {
                    book.Author = request.Author;
                }
                if (!string.IsNullOrEmpty(request.Genre))
                {
                    book.Genre = request.Genre;
                }
                if (request.Price > 0)
                {
                    book.Price = request.Price;
                }
                if (request.Stock > 0)
                {
                    book.Stock = request.Stock;
                }
                book.UpdatedAt = DateTime.Now;

            }

            return (book);
            }
        
        return null;
    }

    //DELETE
    public bool DeleteBook(Guid id)
    {
        int index = 0;
        foreach (Book book in library)
        {
            if (id == book.Id)
            {
                library.RemoveAt(index);
                return true;
            }
            index++;

        }
        return false;
    }

};

