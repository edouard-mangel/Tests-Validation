namespace TodoApi;

public class TodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Todo Add(Todo todo)
    {
        _context.Todos.Add(todo);
        _context.SaveChanges();
        return todo;
    }

    public Todo? GetById(int id)
    {
        return _context.Todos.Find(id);
    }

    public List<Todo> GetAll()
    {
        return _context.Todos.ToList();
    }

    public Todo Update(Todo todo)
    {
        _context.Todos.Update(todo);
        _context.SaveChanges();
        return todo;
    }

    public bool Delete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null) return false;

        _context.Todos.Remove(todo);
        _context.SaveChanges();
        return true;
    }
}
