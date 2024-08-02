
using Stories.Service;
using Stories.Domain;
using Stories.Infrastructure;
using Stories.Repository;
using Stories.API;
using Microsoft.AspNetCore.Authentication;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IStoryService, StoryService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IStoryRepository, StoryRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDomainModelValidator, DomainModelValidator>();

builder.Services.AddSingleton<IHnClient, HnClient>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

var configSettings = builder.Configuration.GetSection(StoriesSettings.OptionKey).Get<StoriesSettings>();

builder.Services.AddHttpClient(HnClient.ClientName, httpClient =>
{
    httpClient.BaseAddress = new Uri(configSettings!.BaseUrl);
});

builder.Services.AddAuthentication();
var app = builder.Build();
app.UseHttpsRedirection();

app.UseCors(applicationBuilder =>
{
    applicationBuilder.AllowAnyMethod();
    applicationBuilder.AllowAnyHeader();
    applicationBuilder.AllowCredentials();
    applicationBuilder.SetIsOriginAllowed(_ => true);
});

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();


app.MapControllers();

app.Run();

