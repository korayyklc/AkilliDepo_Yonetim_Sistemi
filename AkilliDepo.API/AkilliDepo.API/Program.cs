using AkilliDepo.API.Data;
using AkilliDepo.API.Managers;
using AkilliDepo.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. CORS Politikasını Tanımlıyoruz (Frontend bağlantısına izin veriyoruz)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// 2. Veritabanı bağlantımızı sisteme kaydediyoruz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Repository ve Manager katmanlarımızı tanıtıyoruz
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductManager, ProductManager>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// 4. CORS Politikasını Aktif Ediyoruz (Routing'den önce olmalı)
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();