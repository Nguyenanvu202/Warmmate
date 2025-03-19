using System.Linq.Expressions;
using API.DTOs;
using API.Error;
using AutoMapper;
using Core.IRepository;
using Core.IRepository.ProductRelateRepo;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IItemRepo, ItemRepo>();
builder.Services.AddScoped(typeof(IGenericRepo<>),typeof(GenericRepo<>));
builder.Services.AddCors();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var connString = builder.Configuration.GetConnectionString("Redis") 
    ?? throw new Exception("Cannot get read the redis connection string");
    var configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddSingleton<ICartService, CartService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<GenericMappingProfiles>();
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new GenericMappingProfiles());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
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
