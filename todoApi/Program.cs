using Microsoft.EntityFrameworkCore;
using todoApi.Data;
using todoApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt=> opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));
var app = builder.Build();

//app.UseHttpsRedirection();

app.MapGet("api/todo", async (AppDbContext context) =>
{
    var items = await context.ToDos.ToListAsync();

    return Results.Ok(items);
});

app.MapPost("api/todo", async (AppDbContext context, ToDo toDo) =>
{
    await context.ToDos.AddAsync(toDo);

    await context.SaveChangesAsync();

    return Results.Created($"api/todo/{toDo.Id}", toDo);
});

app.MapPut("api/todo/{id}", async (AppDbContext context, int id, ToDo toDo) =>
{
    var todoModel = await context.ToDos.FirstOrDefaultAsync(t => t.Id == id);

    if (todoModel == null)
    {
        return Results.NotFound();
    }

    todoModel.ToDoName = toDo.ToDoName;

    await context.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("api/todo/{id}", async (AppDbContext context, int id) =>
{
    var todoModel = await context.ToDos.FirstOrDefaultAsync(t => t.Id == id);

    if (todoModel == null)
    {
        return Results.NotFound();
    }

    context.ToDos.Remove(todoModel);

    await context.SaveChangesAsync();

    return Results.NoContent();
});
app.Run();

