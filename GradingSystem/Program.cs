using GradingSystem.BLL.BLLLogic;
using GradingSystem.BLL.Filters;
using GradingSystem.BLL.Services;
using GradingSystem.DAL.Abstract.Interface;
using GradingSystem.DAL.Concrete;
using GradingSystem.DAL.Logic;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IQuizDAL, QuizBLL>();
builder.Services.AddScoped<IStudentReportDAL, StudentReportBLL>();
builder.Services.AddScoped<IProjectDAL, ProjectBLL>();
builder.Services.AddScoped<IStudentDAL, StudentBLL>();
builder.Services.AddScoped<ICustomerCourse, CustomerCourseBLL>();
builder.Services.AddScoped<IStudentProjectDAL, StudentProjectBLL>();
builder.Services.AddScoped<ICourseDAL, CourseBLL>();
builder.Services.AddScoped<ICustomerDAL, CustomerBLL>();
builder.Services.AddScoped<IStudentQuiz, StudentQuizBLL>();
builder.Services.AddScoped<CustomerActionFilter>();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(a => a.WithOrigins().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromSeconds(15);
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
