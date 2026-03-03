using Microsoft.AspNetCore.Mvc;

namespace TodoApi;

[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{
    private readonly TodoService _service;
    private readonly TodoRepository _repository;

    public TodosController(TodoService service, TodoRepository repository)
    {
        _service = service;
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<List<TodoDto>> GetAll()
    {
        var todos = _repository.GetAll();
        return Ok(todos.Select(ToDto).ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<TodoDto> GetById(int id)
    {
        var todo = _repository.GetById(id);
        if (todo is null) return NotFound();
        return Ok(ToDto(todo));
    }

    [HttpPost]
    public ActionResult<TodoDto> Create([FromBody] CreateTodoRequest request)
    {
        var todo = _service.CreateTodo(request.Title, request.Description, request.DueDate);
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, ToDto(todo));
    }

    [HttpPut("{id}/complete")]
    public ActionResult<TodoDto> Complete(int id)
    {
        try
        {
            var todo = _service.CompleteTodo(id);
            return Ok(ToDto(todo));
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var deleted = _repository.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    private static TodoDto ToDto(Todo todo) => new()
    {
        Id = todo.Id,
        Title = todo.Title,
        Description = todo.Description,
        IsCompleted = todo.IsCompleted,
        // Bug delibere : conversion en heure locale perd le Kind UTC
        DueDate = todo.DueDate.HasValue
            ? DateTime.SpecifyKind(todo.DueDate.Value, DateTimeKind.Unspecified)
            : null,
        CreatedAt = todo.CreatedAt
    };
}

public class CreateTodoRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
}

public class TodoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
