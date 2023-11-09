using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using SmartManager.Brokers.DateTimes;
using SmartManager.Brokers.Loggings;
using SmartManager.Brokers.Spreadsheets;
using SmartManager.Brokers.Storages;
using SmartManager.Models.BotConfigurations;
using SmartManager.Services.Foundations.Attendances;
using SmartManager.Services.Foundations.ConfigurWebhook;
using SmartManager.Services.Foundations.Groups;
using SmartManager.Services.Foundations.GroupsStatistics;
using SmartManager.Services.Foundations.Payments;
using SmartManager.Services.Foundations.PaymentStatistics;
using SmartManager.Services.Foundations.Spreadsheets;
using SmartManager.Services.Foundations.Statistics;
using SmartManager.Services.Foundations.Students;
using SmartManager.Services.Foundations.StudentsStatistics;
using SmartManager.Services.Foundations.TelegramBots;
using SmartManager.Services.Processings.Attendances;
using SmartManager.Services.Processings.Groups;
using SmartManager.Services.Processings.GroupsStatistics;
using SmartManager.Services.Processings.Payments;
using SmartManager.Services.Processings.PaymentStatistics;
using SmartManager.Services.Processings.Spreadsheets;
using SmartManager.Services.Processings.Statistics;
using SmartManager.Services.Processings.Students;
using SmartManager.Services.Processings.StudentsStatistics;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add builder to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<ISpreadsheetBroker, SpreadsheetBroker>();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
builder.Services.AddTransient<IDateTimeBroker, DateTimeBroker>();

builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IAttendanceService, AttendanceService>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IStatisticService, StatisticService>();
builder.Services.AddTransient<ISpreadsheetService, SpreadsheetService>();
builder.Services.AddTransient<IPaymentStatisticService, PaymentStatisticService>();
builder.Services.AddTransient<IStudentsStatisticService, StudentsStatisticService>();
builder.Services.AddTransient<IGroupsStatisticService, GroupsStatisticService>();
builder.Services.AddScoped<ITelegramBotService, TelegramBotService>();

builder.Services.AddTransient<IStudentProcessingService, StudentProcessingService>();
builder.Services.AddTransient<IAttendanceProcessingService, AttendanceProcessingService>();
builder.Services.AddTransient<IGroupProcessingService, GroupProcessingService>();
builder.Services.AddTransient<IPaymentProcessingService, PaymentProcessingService>();
builder.Services.AddTransient<IStatisticProcessingService, StatisticProcessingService>();
builder.Services.AddTransient<ISpreadsheetsProcessingService, SpreadsheetsProcessingService>();
builder.Services.AddTransient<IPaymentStatisticsProccessingService, PaymentStatisticsProccessingService>();
builder.Services.AddTransient<IStudentsStatisticProccessingService, StudentsStatisticProccessingService>();
builder.Services.AddTransient<IGroupsStatisticProccessingService, GroupsStatisticProccessingService>();
var app = builder.Build();

builder.Services.AddHostedService<ConfigureWebhook>();

    var botConfig = new BotConfiguration();
builder.Services.AddHttpClient("tgWebhook")
        .AddTypedClient<ITelegramBotClient>(httpClient =>
        new TelegramBotClient(botConfig.Token, httpClient));

builder.Services.AddControllers()
    .AddNewtonsoftJson();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
