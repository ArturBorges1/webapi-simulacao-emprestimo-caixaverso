using Microsoft.AspNetCore.Http;
using Caixaverso.Backend.API.Middlewares;
using System.Text.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Caixaverso.Backend.Tests.API.Middlewares
{
    public class ExceptionHandlingMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_SemExcecao_DeveChamarProximoMiddleware()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var nextCalled = false;

            RequestDelegate next = ctx =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = new ExceptionHandlingMiddleware(next);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.True(nextCalled);
        }

        [Fact]
        public async Task InvokeAsync_ComExcecao_DeveRetornarProblemDetailsJson()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;
            context.Request.Path = "/api/teste";

            var exception = new Exception("Erro simulado");

            RequestDelegate next = ctx => throw exception;

            var middleware = new ExceptionHandlingMiddleware(next);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            responseStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(responseStream).ReadToEnd();

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseBody, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            Assert.Equal("Erro interno no servidor", problemDetails?.Title);
            Assert.Equal("Erro simulado", problemDetails?.Detail);
            Assert.Equal("https://httpstatuses.com/500", problemDetails?.Type);
            Assert.Equal("/api/teste", problemDetails?.Instance);
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
            Assert.Equal("application/problem+json", context.Response.ContentType);
        }
    }
}
