using LineBuyCart.Models;
using LineBuyCart.Service;
using LineBuyCart.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(8,0,0));
builder.Services.AddDbContext<ShoopingContext>(option =>
{
    option.UseMySql(builder.Configuration.GetConnectionString("MyContext"),
        serverVersion);
});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<OrderListServices, OrderListServices>();
builder.Services.AddScoped<ApplicationServices, ApplicationServices>();
builder.Services.AddScoped<UserInfoService, UserInfoService>();
builder.Services.AddScoped<OrderFlowServices, OrderFlowServices>();
builder.Services.AddScoped<OrderConfirmServices, OrderConfirmServices>();
builder.Services.AddScoped<HttpServices, HttpServices>();

builder.Services.AddTransient<ShoopingContext, ShoopingContext>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
// HTTP Request
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

