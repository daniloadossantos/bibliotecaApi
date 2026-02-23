using BookstoreRocketseat.Communications.Requests;
using BookstoreRocketseat.Communications.Responses;
using BookstoreRocketseat.Entities;
using BookstoreRocketseat.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreRocketseat.Controllers;

[Route("api/books/")]
[ApiController]

public class BookController : ControllerBase
{
    //Campo para guardar o Service
    private readonly BookService _bookService;

    //Construtor da classe oara receber o Service como parâmetro
    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }
    /// <summary>
    /// Cria um novo livro na livraria
    /// </summary>
    /// <param name="request">Dados do livro a ser criado (título, autor, gênero, preço e estoque)</param>
    /// <returns>Retorna o livro criado com ID e data de criação</returns>
    /// <response code="201">Livro criado com sucesso</response>
    /// <response code="400">Dados inválidos ou incompletos</response>
    /// <response code="409">Conflito - Livro com mesmo título e autor já existe</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPost]
    [Route("new-book")]
    [Tags("Criar Novos Livros")]
    [ProducesResponseType(typeof(ResponseBookJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    
    public IActionResult Create([FromBody] RequestNewBookJson request)
    {
        // 1. Verificar se as validações do Request passaram
        // Validação dos 'Data Annotations' do RequestNewBookJson
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ResponseBookJson response = _bookService.Create(request);

        // 3. Se houver duplicação (título+autor) → retornar 409 Conflict

        if (response == null)
        {
            return Conflict(new { message = "Livro com mesmo título e autor já existe" });
        }

        // 4. Se tudo OK → retornar 201 Created
        return Created(string.Empty, response);
    }

    /// <summary>
    /// Lista todos os livros cadastrados na livraria
    /// </summary>
    /// <returns>Retorna lista com todos os livros</returns>
    /// <response code="200">Lista de livros retornada com sucesso</response>
    /// <response code="500">Erro interno do servidor</response>

    [HttpGet]
    [Tags("Biblioteca")]
    [ProducesResponseType(typeof(ResponseBookJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseBookJson),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseBookJson),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseBookJson),StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        List<ResponseBookJson> allBooksArray = new List<ResponseBookJson>();
        allBooksArray = _bookService.GetAllBooks();
        return Ok(allBooksArray);
    }


    /// <summary>
    /// Busca um livro específico pelo seu ID
    /// </summary>
    /// <param name="id">ID único do livro (formato GUID)</param>
    /// <returns>Retorna os dados do livro encontrado</returns>
    /// <response code="200">Livro encontrado com sucesso</response>
    /// <response code="404">Livro não encontrado com o ID informado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpGet]
    [Tags("Biblioteca")]
    [ProducesResponseType(typeof(ResponseBookJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseBookJson),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseBookJson),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseBookJson),StatusCodes.Status500InternalServerError)]
    [Route("{id}")]
    public IActionResult GetBookById(Guid id)
    {
        ResponseBookJson? _bookFound = _bookService.GetBookById(id);

        if (_bookFound == null)
        {
            return NotFound();
        }

        return Ok(_bookFound);
    }

    //Para atualizar informações de um livro

    /// <summary>
    /// Atualiza as informações de um livro existente
    /// </summary>
    /// <param name="id">ID do livro a ser atualizado</param>
    /// <param name="request">Novos dados do livro (todos os campos são obrigatórios)</param>
    /// <returns>Retorna o livro atualizado com a data de modificação</returns>
    /// <response code="200">Livro atualizado com sucesso</response>
    /// <response code="400">Dados inválidos ou incompletos</response>
    /// <response code="404">Livro não encontrado com o ID informado</response>
    /// <response code="500">Erro interno do servidor</response>
    [HttpPut]
    [Tags("Editar/Remover Livros")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Book), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Book),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Book),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Book),StatusCodes.Status500InternalServerError)]
    [Route("{id}")]

    public IActionResult UpdateBook([FromRoute] Guid id, [FromBody] RequestUpdateBookJson request)
    {
        Book response = _bookService.UpdateBook(id, request);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(new { message = "Livro atualizado com sucesso.", response });
    }

    /// <summary>
    /// Remove um livro da livraria
    /// </summary>
    /// <param name="id">ID do livro a ser removido</param>
    /// <returns>Mensagem de confirmação da exclusão</returns>
    /// <response code="200">Livro removido com sucesso</response>
    /// <response code="404">Livro não encontrado com o ID informado</response>
    /// <response code="500">Erro interno do servidor</response>

    [HttpDelete]
    [Tags("Editar/Remover Livros")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("{id}")]
    public IActionResult DeleteBook([FromRoute] Guid id)
    {
        bool response = _bookService.DeleteBook(id);

        if (response == false)
        {
            return NotFound();
        }

        return Ok($"Livro [ID:{id}] foi removido com sucesso.");
    }
}