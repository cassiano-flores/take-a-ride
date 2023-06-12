using MeLevaAi.Api.Contracts;
using MeLevaAi.Api.Middlewares;
using MeLevaAi.Api.Validations;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

// data validation customizado
builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.InvalidModelStateResponseFactory = actionContext =>
    {
        var notificacoes = actionContext.ModelState.Values.SelectMany(
            e => e.Errors.Select(erro => new Notification(erro.ErrorMessage))
        );

        var response = new ErrorResponse(notificacoes.ToList());

        return new ConflictObjectResult(response);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors(b => b.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
