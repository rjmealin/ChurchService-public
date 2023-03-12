using ChurchServiceApi;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwaggerUI();
}

app.UseCors( x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed( origin => true));

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500; // or another Status accordingly to Exception Type
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            var ex = error.Error;

            await context.Response.WriteAsync(new ErrorDto()
            {
                Code = 500,
                Message = ex.Message // or your custom message
                                     // other custom data
            }.ToString(), Encoding.UTF8);
        }
    });
});

app.MapControllers();

app.Run();
