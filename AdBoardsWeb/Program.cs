using AdBoards.ApiClient;
using AdBoardsWeb.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddAuthentication(AuthSchemes.Cookie)
    .AddCookie(AuthSchemes.Cookie, options =>
    {
        options.Cookie.Name = "auth";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
        options.LoginPath = "/Authorization";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.SiteUser, pb =>
    {
        pb.RequireAuthenticatedUser();
        pb.AuthenticationSchemes.Add(AuthSchemes.Cookie);
    });
});

builder.Services.AddControllersWithViews();

var apiClient = new AdBoardsApiClient(builder.Configuration["BaseApiUri"]!);
builder.Services.AddSingleton(apiClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.Use(async (context, next) =>
{
    var api = context.RequestServices.GetRequiredService<AdBoardsApiClient>();
    api.Jwt = context.User.Claims.FirstOrDefault(x => x.Type == "jwt")?.Value;
    await next(context);
});

app.MapControllerRoute("default", "{controller=Home}/{action=AdsPage}");

app.Run();