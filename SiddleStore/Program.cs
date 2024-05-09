using BusinessObject;
using DataAcess.DAO;
using DataAcess.Repository.Customer;
using DataAcess.Repository.DailyRevenue;
using DataAcess.Repository.Order;
using DataAcess.Repository.OrderDetail;
using DataAcess.Repository.Product;
using DataAcess.Repository.Store;
using DataAcess.Repository.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SiddleStore.BackgroundServices;
using SiddleStore.Configurations;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddHostedService<BackgroundServices>();

// Register DbContext
builder.Services.AddDbContext<SiddleStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlData"));
}, ServiceLifetime.Scoped);

// AddScoped
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<DailyRevenueDAO>();
builder.Services.AddScoped<IDailyRevenueRepository, DailyRevenueRepository>();

builder.Services.AddScoped<IStoreRepository, StoreRepository>();

builder.Services.AddScoped<CustomerDAO>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<OrderDAO>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<OrderDetailDAO>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

builder.Services.AddScoped<ProductDAO>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Authen in swagger
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

// Jwt
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config.AppSetting["JWT:ValidIssuer"],
        ValidAudience = config.AppSetting["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.AppSetting["JWT:Secret"]))
    };
});

// Ignore cycles ef core
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authen
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
