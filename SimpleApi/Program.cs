var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// Get all products
app.MapGet("/Products", () =>
{
    return DatabaseManager.GetProducts();
})
.WithName("GetProducts");

// Create a new product
app.MapPost("/Products", (Product product) =>
{
    DatabaseManager.CreateProduct(product);
    return Results.Created($"/Products/{product.ProductId}", product);
})
.WithName("CreateProduct");

app.Run();