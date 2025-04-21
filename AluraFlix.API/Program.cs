using AluraFlix.API.Endpoints;
using AluraFlix.Banco;
using AluraFlix.Modelos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AluraflixContext>((options) => {
    options
        .UseLazyLoadingProxies()
        .UseSqlServer(builder.Configuration["ConnectionString:DefaultConnection"]);
});

builder.Services.AddTransient(typeof(Video));
builder.Services.AddTransient(typeof(AluraflixDal<Video>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseHttpsRedirection();
app.AddEndpointsVideo();
app.Run();
