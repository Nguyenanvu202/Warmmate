using API.Error;
using Core.Entities;
using Core.IRepository;
using Core.IRepository.ProductRelateRepo;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IItemRepo, ItemRepo>();
builder.Services.AddScoped(typeof(IGenericRepo<>),typeof(GenericRepo<>));
builder.Services.AddCors();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

//app.UseHttpsRedirection(); //that is using for redirect http to https

//app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
//allow header: any http header will be allow, Method: any method will me allow (post, put, get...), WithOrigins: another url
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));
app.MapControllers();
try
{
    using var scope = app.Services.CreateScope();
    
        var services = scope.ServiceProvider;
        var _storeContext = services.GetRequiredService<StoreContext>();
        await _storeContext.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(_storeContext);
    
}
catch (System.Exception ex)
{
    
    Console.WriteLine(ex);
    throw;
}
app.Run();
