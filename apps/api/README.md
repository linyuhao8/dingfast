當然可以！以下是一份針對你 ASP.NET Core 專案（包含 Pizza Model、Controller、DbContext、Swagger）設計的 README 範本，簡潔說明操作步驟與常用指令：

---

# ContosoPizza ASP.NET Core API

此專案為示範使用 Entity Framework Core 連接 PostgreSQL 的 Pizza 資料管理 API。包含 Pizza 資料模型、資料庫遷移、Swagger 文件、Dto。

---

## 🚀 啟動專案前準備

### 1. Clone 專案並進入資料夾

```bash
git clone https://github.com/linyuhao8/asp.net-start-template.git
cd asp.net-start-template/
```

### 2. 還原 NuGet 套件

```bash
dotnet restore
```

### 3. 編輯資料庫連線字串（`appsettings.json`）

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ContosoPizzaDb;Username=postgres;Password=your_password"
  }
}

```

### 4. 建立 PostgreSQL 資料庫（如尚未建立）

使用 psql 或 pgAdmin 執行：

```sql
CREATE DATABASE ContosoPizzaDb;
```

---

## 🛠️ 同步資料庫

終端機執行

```bash
# 如果是第一次初始化，會建立資料表
dotnet ef database update
```

你會看到類似這樣的訊息在終端機，已經成功同步資料庫了

```php
ail: Microsoft.EntityFrameworkCore.Database.Command[20102]
      Failed executing DbCommand (7ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "MigrationId", "ProductVersion"
      FROM "__EFMigrationsHistory"
      ORDER BY "MigrationId";
Failed executing DbCommand (7ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
SELECT "MigrationId", "ProductVersion"
FROM "__EFMigrationsHistory"
ORDER BY "MigrationId";
info: Microsoft.EntityFrameworkCore.Migrations[20411]
      Acquiring an exclusive lock for migration application. See https://aka.ms/efcore-docs-migrations-lock for more information if this takes too long.
Acquiring an exclusive lock for migration application. See https://aka.ms/efcore-docs-migrations-lock for more information if this takes too long.
info: Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20250711152718_InitialCreate'.
Applying migration '20250711152718_InitialCreate'.
Done.
```

## 開啟專案

```php
# start 
dotnet run
```

---

## 📑 Swagger 文件

啟動專案後，瀏覽器開啟：

```
http://localhost:{port}/swagger
```

查看 API 文件並測試介面。

---

## 🎯 API 功能

接下來可以開啟swagger操作API

| 路徑 | 功能 | 方法 |
| --- | --- | --- |
| `/api/pizzas` | 取得所有 pizza | GET |
| `/api/pizzas/{id}` | 取得指定 pizza | GET |
| `/api/pizzas` | 新增 pizza | POST |
| `/api/pizzas/{id}` | 更新 pizza | PUT |
| `/api/pizzas/{id}` | 刪除 pizza | DELETE |

---

## 🔄 重置migration和model
如果需要建立新的model和migration步驟請參考：

### 1. **刪除舊 Migration 檔案**

手動刪除 `/Migrations` 資料夾（或你放 migration 的地方），或是：

```bash
rm -r Migrations
```

> ※ 不要刪到 `DbContext` 和 `Model`。

### 2. **重新產生 Migration**
修改 Model 和 DBContext 後，然後產生新的初始 Migration：

```bash
dotnet ef migrations add InitialCreate
```

> 🔸 建議名稱就叫 `InitialCreate`，表示這是「重開機」後的第一版。

---

### 3. **更新資料庫**

然後同步你的資料庫結構：

```bash
dotnet ef database update
```

這時會依照你目前 Model 生成的 Migration 建立所有資料表。


---

## 🔧 常用指令整理

| 指令 | 功能 |
| --- | --- |
| `dotnet run` | 啟動 ASP.NET Core 專案 |
| `dotnet ef migrations add Name` | 新增 migration |
| `dotnet ef database update` | 套用 migration，同步資料庫 |
| `dotnet ef migrations list` | 列出所有 migration |
| `dotnet ef migrations remove` | 刪除最後一筆 migration |

---

## 📝 Model 範例（Pizza）

```csharp
public class Pizza
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

```
