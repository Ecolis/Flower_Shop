using Flower_Shop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Регистрируем наш склад как Singleton
// Singleton - значит, что склад создастся один раз и будет жить всё время работы программы
builder.Services.AddSingleton<IFlowerRepository, FlowerRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
