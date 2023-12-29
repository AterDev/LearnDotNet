var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

// Ĭ��·�ɷ���
app.Map("/", () => "Hello World!");
// ������������
app.Map("/{name}", (string name) => "Hello " + name);

// ʹ�ÿ�����Ĭ��·��ģ��
//app.MapDefaultControllerRoute();


app.MapControllers();


app.Run();
