using System.Linq.Expressions;
using API.DTOs;
using API.Error;
using AutoMapper;
using Core.Entities;
using Core.Entities.Momo;
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

//DbContext
builder.Services.AddDbContext<StoreContext>(opt =>{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//MOMO
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();
//GenericRepo
builder.Services.AddScoped(typeof(IGenericRepo<>),typeof(GenericRepo<>));

//Cors
builder.Services.AddCors();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

//Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var connString = builder.Configuration.GetConnectionString("Redis") 
    ?? throw new Exception("Cannot get read the redis connection string");
    var configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<GenericMappingProfiles>();
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new GenericMappingProfiles());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//Service
builder.Services.AddSingleton<ICartService, CartService>();

//Authorization
builder.Services.AddIdentityApiEndpoints<AppUser>().AddEntityFrameworkStores<StoreContext>();
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));
app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();
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
