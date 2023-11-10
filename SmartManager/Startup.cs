//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartManager.Brokers.DateTimes;
using SmartManager.Brokers.Loggings;
using SmartManager.Brokers.Spreadsheets;
using SmartManager.Brokers.Storages;
using SmartManager.Brokers.Telegrams;
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
using SmartManager.Services.Foundations.TelegramInformations;
using SmartManager.Services.Processings.Attendances;
using SmartManager.Services.Processings.Groups;
using SmartManager.Services.Processings.GroupsStatistics;
using SmartManager.Services.Processings.Payments;
using SmartManager.Services.Processings.PaymentStatistics;
using SmartManager.Services.Processings.Spreadsheets;
using SmartManager.Services.Processings.Statistics;
using SmartManager.Services.Processings.Students;
using SmartManager.Services.Processings.StudentsStatistics;
using SmartManager.Services.Processings.TelegramInformations;
using Telegram.Bot;

namespace SmartManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            BotConfiguration = Configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
        }

        public IConfiguration Configuration { get; }
        public BotConfiguration BotConfiguration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            ConfigureBrokers(services);
            ConfigureFoundationServices(services);
            ConfigureProcessingServices(services);
            ConfigureTelegramService(services);
        }

        public void ConfigureTelegramService(IServiceCollection services)
        {
            services.AddHostedService<ConfigureWebhook>();

            services.AddHttpClient("tgWebhook")
                .AddTypedClient<ITelegramBotClient>(httpClient =>
                new TelegramBotClient(BotConfiguration.Token, httpClient));

            services.AddScoped<TelegramBotService>();

            services.AddControllers().AddNewtonsoftJson();

        }

        private static void ConfigureBrokers(IServiceCollection services)
        {
            services.AddDbContext<IStorageBroker, StorageBroker>();
            services.AddTransient<ISpreadsheetBroker, SpreadsheetBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
            services.AddTransient<IDateTimeBroker, DateTimeBroker>();
            services.AddTransient<ITelegramBroker,  TelegramBroker>();
        }

        private static void ConfigureProcessingServices(IServiceCollection services)
        {
            services.AddTransient<IStudentProcessingService, StudentProcessingService>();
            services.AddTransient<IAttendanceProcessingService, AttendanceProcessingService>();
            services.AddTransient<IGroupProcessingService, GroupProcessingService>();
            services.AddTransient<IPaymentProcessingService, PaymentProcessingService>();
            services.AddTransient<IStatisticProcessingService, StatisticProcessingService>();
            services.AddScoped<ISpreadsheetsProcessingService, SpreadsheetsProcessingService>();
            services.AddTransient<IPaymentStatisticsProccessingService, PaymentStatisticsProccessingService>();
            services.AddTransient<IStudentsStatisticProccessingService, StudentsStatisticProccessingService>();
            services.AddTransient<IGroupsStatisticProccessingService, GroupsStatisticProccessingService>();
            services.AddTransient<ITelegramInformationProcessingService, TelegramInformationProcessingService>();
        }

        private static void ConfigureFoundationServices(IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IAttendanceService, AttendanceService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IStatisticService, StatisticService>();
            services.AddTransient<ISpreadsheetService, SpreadsheetService>();
            services.AddTransient<IPaymentStatisticService, PaymentStatisticService>();
            services.AddTransient<IStudentsStatisticService, StudentsStatisticService>();
            services.AddTransient<IGroupsStatisticService, GroupsStatisticService>();
            services.AddTransient<ITelegramInformationService, TelegramInformationService>();
            services.AddTransient<ITelegramBotService, TelegramBotService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                var token = BotConfiguration.Token;

                endpoints.MapControllerRoute(
                    name: "tgWebhook",
                    pattern: $"bot/{token}",
                    new { controller = "Webhook", action = "Post" });
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
