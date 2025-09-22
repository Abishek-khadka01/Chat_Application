

using Chat_Application.Routes;
using StackExchange.Redis;



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

//Adding the redis connection 
builder.Services.AddSingleton<IConnectionMultiplexer>(options=>
{
    var  config = new ConfigurationOptions
    {
        EndPoints = { "localhost:6379" },
        User = "yourUsername",
        Password = "yourPassword"
    };
    return ConnectionMultiplexer.Connect(config);
});


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