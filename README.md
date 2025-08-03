# 🚀 DingFast 專案啟動教學

本教學適用於初次 clone 專案後的啟動步驟，包含後端 ASP.NET Core、前端 Next.js、PostgreSQL 資料庫與 Turbo monorepo 架構。

---

## ✅ 環境需求

- [Node.js](https://nodejs.org/) 16+
- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [PostgreSQL](https://www.postgresql.org/)（本地安裝）
- Turbo CLI（需另外安裝）

---

## 📦 Step 1: 安裝 Turbo CLI

```bash
npm install -g turbo
```

---

## ⚙️ Step 2: 建立 appsettings 檔案與資料庫設定

進入後端目錄並新增 `appsettings.json`：

```bash
cd apps/api
mkdir appsettings.josn
```

建立設定檔內容如下（請依照你本地資料庫資訊修改）：

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=dingfast;Username=你的使用者;Password=你的密碼"
  },
  "FrontendUrl": "http://localhost:3000",
  "Jwt": {
    "Issuer": "dingfast.auth",
    "Audience": "dingfast.client",
    "Key": "超長超安全的密鑰1234567890"
  }
}
```

✅ 若本地尚未建立資料庫，請使用：

```bash
createdb dingfast
```

---

## 🛠 Step 3: 還原 .NET 套件與更新資料庫

在 `apps/api` 下執行：

```bash
dotnet restore
dotnet ef database update
```

---

## 🌐 Step 4: 安裝前端依賴

切換至前端專案並安裝套件：

```bash
cd apps/web
rm package-lock.json # 若存在，避免與 root 衝突
npm install
```

---

## 📁 Step 5: 安裝 Root 依賴（Turbo monorepo）

回到專案根目錄安裝整包依賴：

```bash
cd ../.. # 回到根目錄
npm install
```

---

## 🚀 Step 6: 啟動整個專案

使用 Turbo 一鍵啟動所有服務：

```bash
npm run dev
```

成功後應可看到：

- 前端（Next.js）啟動於： http://localhost:3000
- 後端（ASP.NET Core API）啟動於： http://localhost:5208（或其他 port）

---

## 🧹 建議清理動作

刪除多餘的 lockfile 避免依賴混淆：

```bash
rm apps/web/package-lock.json
```

---

## 🧯 常見問題排查

| 問題                          | 解法說明                                                      |
| ----------------------------- | ------------------------------------------------------------- |
| `JWT secret 為 null`          | 確認 `appsettings.Development.json` 有設好 `Jwt.Key`          |
| `PostgreSQL 無法連線`         | 確認資料庫已啟動，帳號密碼正確，且資料庫存在                  |
| `找不到 turbo`                | 請重新執行 `npm install -g turbo`                             |
| `.NET CLI 顯示 EF 指令不存在` | 確保你已安裝 EF CLI：`dotnet tool install --global dotnet-ef` |

---

## 🔄 重置 Migration 和資料庫

若你需要清空現有資料庫並重建 migration，可依以下步驟操作：

### 1️⃣ 刪除舊 Migration 檔案

```bash
rm -r Migrations
```

> ✅ **不要刪除 DbContext.cs 與 Model 類別**

---

### 2️⃣ 建立新的 Migration

```bash
dotnet ef migrations add InitialCreate
```

---

### 3️⃣ 更新資料庫

```bash
dotnet ef database update
```

---

如有問題請聯絡維護者或團隊協作者。
