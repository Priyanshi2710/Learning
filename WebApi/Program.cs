using Microsoft.EntityFrameworkCore;
using Domain.Models;
using WebApi.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using Service;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();



builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
    //options.UseInMemoryDatabase("Connectionstring");
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;


});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default",
               pattern: "{controller=Employee} ");
app.Run();
