using DataAccess.AttendenceRepositories;
using DataAccess.EmployeeRepositories;
using DataAccess.EmpRequestLeaveRepositories;
using DataAccess.EmpResignationRequestsRepositories;
using DataAccess.ResignationRequestRepositories;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Prn221_group_project.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HRM_SWD392Context>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("HRM_SWD392"));
});
builder.Services.AddSession();

//builder.Services.AddControllers().AddJsonOptions(x =>
//                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmpRequestLeaveRepository, EmpRequestLeaveRepository>();
builder.Services.AddScoped<IEmpResignationRequestsRepository, EmpResignationRequestsRepository>();
builder.Services.AddScoped<IAttendenceRepository, AttendenceRepository>();
builder.Services.AddScoped<IResignationRequestRepository, ResignationRequestRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseCustomAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
