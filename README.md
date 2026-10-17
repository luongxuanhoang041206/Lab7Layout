# Lab7Layout

An ASP.NET Core MVC layout and catalogue-management assignment implemented with .NET 10, Razor views, AdminLTE 3, and SQLite.

## Run locally

```powershell
dotnet restore
dotnet run
```

The storefront is available at `/`. The administration area starts at `/Admin` and provides CRUD screens for categories, products, banners, and journal posts. The local SQLite database (`lab7layout.db`) and a small set of example catalogue items are created on first run.

All MVC controller classes use the `LXH` prefix as requested. The public storefront and Admin area have separate Razor layouts; AdminLTE, Bootstrap 4, Font Awesome, and jQuery assets are served locally from `wwwroot`.
