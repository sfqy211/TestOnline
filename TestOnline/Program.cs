using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

// ���SqlSugar����
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

// ע��֧�ֿ�������ͼ����
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
// ������̬��Դ�����м��
app.UseStaticFiles();

// ʹ��·��
app.UseRouting();

app.UseAuthorization();

// Ĭ��·�����ã������Ϊȱʡֵ����
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Validate}/{id?}");

app.Run();