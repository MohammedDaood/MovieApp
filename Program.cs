using Microsoft.EntityFrameworkCore;
using MovieApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// for database connection Configuration
//________________________________________________//
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
//________________________________________________//

//≈–«  —Ìœ √‰ Ì Ê«’· √Ì ›—Ê‰  (React, Angular, Flutter Web) „⁄ «·‹ API°  Õ «Ã CORS:
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});
// › Õ »Ê—  8080 œ«Œ· «·‹ Docker ·ﬂÌ Ì” ÿÌ⁄ √Ì ›—Ê‰  Ì Ê«’· „⁄ «·‹ API:
builder.WebHost.UseUrls("http://0.0.0.0:8080");


var app = builder.Build();

app.UseCors("AllowAll");



//Â–« Ì⁄„· Migration  ·ﬁ«∆Ì œ«Œ· Docker SQL Server.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.

app.UseSwagger();
    app.UseSwaggerUI();         

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
