using MediatR;
using timeZZle.Application;
using timeZZle.Behaviors;
using timeZZle.Components;
using timeZZle.Data;
using timeZZle.Extensions;
using timeZZle.Middlewares;
using timeZZle.Web.Api.Endpoints.Clocks;
using timeZZle.Web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt =>
    opt.AddPolicy("All", pol => pol.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));

builder.Services
    .RegisterApp()
    .RegisterData()
    .RegisterMappings();

builder.Services.AddControllers();
builder.Services.AddEndpoints(typeof(CreateClockEndpoint).Assembly);

// front services : 
//builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:5299") });
builder.Services.AddHttpClient();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.UseCors("All");
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.Seed();
}

// app.MapHealthChecks("health", new HealthCheckOptions
// {
//     ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
// });

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();