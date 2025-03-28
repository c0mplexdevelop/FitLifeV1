using FitLife.Components;
using FitLife.Data;
using FitLife.Models.State;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FitLife.Models.User;
using FitLife.Auth;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddScoped<UserSignUpState>();
builder.Services.AddScoped<AuthService>();


builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseSqlServer(connString)
    //.UseSeeding((context, _) =>
    //{
    //    var testUser = context.Set<User>().FirstOrDefault();
    //    if (testUser == null)
    //    {
    //        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AspNetUsers] ON");
    //        User user = new()
    //        {
    //            Id = -1,
    //            Email = "test@test.com",
    //            NormalizedEmail = "test@test.com".ToUpper(),
    //            FirstName = "John",
    //            MiddleName = null,
    //            LastName = "Doe",
    //            Sex = FitLife.Models.User.Enum.Sex.Male,
    //            DateOfBirth = DateOnly.MinValue,
    //            UserName = "John Doe"
    //        };
    //        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "Test!23");
    //        context.Set<User>().Add(user);
    //        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF");

    //        context.SaveChanges();
    //    }

    //})
    .EnableSensitiveDataLogging()
    );



builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";

})
    .AddCookie("Cookies");

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Set your custom login path here
    options.LoginPath = "/login";
    options.ReturnUrlParameter = "/login";
    options.AccessDeniedPath = "/login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();


using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
context.SeedDataAsync();


app.Run();
