using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserDal, EfUserRepository>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserSessionDal, EfUserSessionRepository>();
builder.Services.AddScoped<IUserSessionService, UserSessionManager>();
builder.Services.AddScoped<IPasswordResetDal, EfPasswordResetRepository>();
builder.Services.AddScoped<IPasswordResetService, PasswordResetManager>();
builder.Services.AddScoped<IHealthGoalDal, EfHealthGoalRepository>();
builder.Services.AddScoped<IHealthGoalService, HealthGoalManager>();
builder.Services.AddScoped<IHealthRecordDal, EFHealthRecordRepository>();
builder.Services.AddScoped<IHealthRecordService, HealthRecordManager>();
builder.Services.AddScoped<IRecommendationDal, EfRecommendationRepository>();
builder.Services.AddScoped<IRecommendationService, RecommendationManager>();
builder.Services.AddScoped<INoteDal, EfNoteRepository>();
builder.Services.AddScoped<INoteService, NoteManager>();

builder.Services.AddDbContext<Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login/Index";
                options.LogoutPath = "/Logout/Index";
            });

builder.Services.AddSingleton(new MailService(
       smtpServer: "smtp.gmail.com",
       smtpPort: 587,
       smtpUsername: "healthexample@gmail.com",
       smtpPassword: "12345a"
   ));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
