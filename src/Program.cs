global using Serilog;
using System.ComponentModel;
using Chat_Application.Routes;



var builder = WebApplication.CreateBuilder(args);

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("./logs/log.txt")
    .CreateLogger();

//add the db context 


// add the rate limiting 


// adding the cors policy 
builder.Services.AddCors(options =>
 options.AddDefaultPolicy(rules =>
     rules.WithOrigins(builder.Configuration["AllowedHosts"])
        .AllowAnyHeader()
  ));


builder.Services.AddAuthorization();


var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();
app.MapUserEndPoints();
app.Run();