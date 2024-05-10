using MailAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(options=>
options.AddDefaultPolicy(policy=>
policy.AllowCredentials()
      .AllowAnyHeader()
      .AllowAnyMethod()
      .SetIsOriginAllowed(x=>true)));
var app = builder.Build();
app.UseCors();
app.UseRouting();
app.MapControllers();
app.MapHub<MessageHub>("/messagehub");
app.Run();
