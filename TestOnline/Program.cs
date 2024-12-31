using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

// 添加SqlSugar服务
builder.Services.AddScoped<ISqlSugarClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("Default");
    return new SqlSugarClient(new ConnectionConfig
    {
        ConnectionString = connectionString,
        DbType = DbType.SqlServer,
        IsAutoCloseConnection = true,
        InitKeyType = InitKeyType.Attribute
    });
});

// 注入支持控制器视图服务
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
// 启动静态资源服务中间件
app.UseStaticFiles();

// 使用路由
app.UseRouting();

app.UseAuthorization();

// 默认路由配置，可理解为缺省值配置
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Validate}/{id?}");

app.Run();