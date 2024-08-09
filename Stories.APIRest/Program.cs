using Stories.Service;
using Stories.Infrastructure;
using Stories.Repository;
using Stories.API;
using Stories.Domain.Validation;
using Stories.Domain.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IStoryService, StoryService>();
builder.Services.AddTransient<IStoryRepository, StoryRepository>();
builder.Services.AddTransient<IDomainModelValidator, DomainModelValidator>();
builder.Services.AddMemoryCache();

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();

});

builder.Services.AddSingleton<IHnClient, HnClient>();

var configSettings = builder.Configuration.GetSection(StoriesSettings.OptionKey).Get<StoriesSettings>();

builder.Services.AddHttpClient(HnClient.ClientName, httpClient =>
{
    httpClient.BaseAddress = new Uri(configSettings!.BaseUrl);
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(applicationBuilder =>
{
    applicationBuilder.AllowAnyMethod();
    applicationBuilder.AllowAnyHeader();
    applicationBuilder.AllowCredentials();
    applicationBuilder.SetIsOriginAllowed(_ => true);
});


app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

