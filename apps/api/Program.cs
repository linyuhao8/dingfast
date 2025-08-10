using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using user.Repositories;
using user.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using auth.Services;
using Application.Merchants.Services;
using Application.Merchants.Repositories;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);
//取得appsettings裡面的環境變數
var frontendUrl = builder.Configuration["FrontendUrl"];



// 註冊 Controller 服務
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // 把 enum 轉成字串輸出
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
//cors設定
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendClient", policy =>
        policy.WithOrigins(frontendUrl ?? "http://localhost:5173") // 允許的網站
              .AllowAnyHeader()
              .AllowAnyMethod()
                .AllowCredentials()
              );
});


//註冊資料庫
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("dingfast-jwt-token"))
                    context.Token = context.Request.Cookies["dingfast-jwt-token"];
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
    {
        Console.WriteLine("Authentication failed: " + context.Exception.Message);
        return Task.CompletedTask;
    }
        };
    });
Console.WriteLine($"Jwt Audience: {builder.Configuration["Jwt:Audience"]}");


// 註冊 User 相關服務
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
builder.Services.AddScoped<IMerchantCategoryRepository, MerchantCategoryRepository>();
builder.Services.AddScoped<MerchantCategoryService>();


// 啟用 Swagger 產生 OpenAPI 文件
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "P API",
        Description = "",
        Version = "v1"
    });
});
builder.Services.AddAuthorization();
var app = builder.Build();

// 測試資料庫連線
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        bool canConnect = db.Database.CanConnect();
        Console.WriteLine($"Database connection success? {canConnect}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}


if (app.Environment.IsDevelopment())
{
    // 啟用 Swagger 中介軟體與 UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

// 使用順序要正確：Routing → Cors → Authorization → Controller
app.UseRouting();
app.UseCors("FrontendClient");
app.UseAuthentication();
app.UseAuthorization();
// app.UseHttpsRedirection(); // 可以先註解

// 把 Controller 路由映射起來
// 會掃描你專案中所有加了 [ApiController] 和 [Route(...)] 的 
// controller 類別，自動把它們的路由對應起來。
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
