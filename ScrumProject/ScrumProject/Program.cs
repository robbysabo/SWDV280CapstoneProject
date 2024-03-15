using ScrumProject;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
startup.Configure(app, builder.Environment);