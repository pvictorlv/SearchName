using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using SearchName.Middlewares;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling
            = ReferenceLoopHandling.Ignore;

        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;

        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;

        options.SerializerSettings.Converters.Add(new StringEnumConverter());

        options.SerializerSettings.Converters.Add(new IsoDateTimeConverter
        {
            DateTimeStyles = DateTimeStyles.AssumeUniversal
        });
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


var options = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
};
app.UseForwardedHeaders(options);


app.UseHttpsRedirection();


app.UseStatusCodePages();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.UseResponseCompression();


app.Run();
