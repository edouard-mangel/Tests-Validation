namespace TodoApi;

public class TodoService
{
    private readonly TodoRepository _repository;

    public TodoService(TodoRepository repository)
    {
        _repository = repository;
    }

    public Todo CreateTodo(string title, string? description, DateTime? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));

        var todo = new Todo
        {
            Title = title.Trim(),
            Description = description,
            DueDate = dueDate,
            CreatedAt = DateTime.UtcNow,
            IsCompleted = false
        };

        return _repository.Add(todo);
    }

    public Todo CompleteTodo(int id)
    {
        var todo = _repository.GetById(id)
            ?? throw new InvalidOperationException($"Todo {id} not found");

        todo.IsCompleted = true;
        return _repository.Update(todo);
    }

    public List<Todo> GetOverdueTodos()
    {
        return _repository.GetAll()
            .Where(t => !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value < DateTime.UtcNow)
            .ToList();
    }
}
