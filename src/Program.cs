

using Chat_Application.Routes;



var builder = WebApplication.CreateBuilder(args);

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("./logs/log.txt")
    .CreateLogger();

//add the db context 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))

);
// add the rate limiting 


// adding the cors policy 
builder.Services.AddCors(options =>
 options.AddDefaultPolicy(rules =>
     rules.WithOrigins(builder.Configuration["AllowedHosts"])
        .AllowAnyHeader()
  ));


// // authentication 
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme =  


// });



// authorization 
builder.Services.AddAuthorization();


var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapUserEndPoints();
app.Run();