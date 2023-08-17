using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViacaoSystemAPI.Persistence;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Com banco de dados
    var connectionString = builder.Configuration.GetConnectionString("TodoEventsCs");
    builder.Services.AddDbContext<TodoDbContext>(a => a.UseSqlServer(connectionString));

    //Sem Banco de Dados
    //builder.Services.AddDbContext<TodoDbContext>(a => a.UseInMemoryDatabase("TodoEventDb"));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception e)
{
    Console.WriteLine($"Erro deu aqui: {e.Message} ");
    new StatusCodeResult(500);
}