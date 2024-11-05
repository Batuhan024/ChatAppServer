//using ChatAppServer.WebAPI.Context;
//using ChatAppServer.WebAPI.Hubs;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

////builder.Services.AddDefaultCors();
//// Add services to the container.
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
//    builder.Configuration.GetConnectionString("SqlServer")));
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddSignalR();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.MapHub<ChatHub>("/chat-hub");

//app.Run();


using ChatAppServer.WebAPI.Context;
using ChatAppServer.WebAPI.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS politikas�n� tan�ml�yoruz
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular uygulaman�z�n URL'si
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // SignalR i�in gerekli
    });
});

// Di�er servisler
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS politikas�n� burada uygulay�n
app.UseCors("AllowAngularApp");
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat-hub");

app.Run();
