var builder = WebApplication.CreateBuilder(args);

var serviceCollection = builder.Services;
{
    serviceCollection.AddControllers();
    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddSwaggerGen();
    serviceCollection.AddRouting(opt => opt.LowercaseUrls = true);
}

var app = builder.Build();
{
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
