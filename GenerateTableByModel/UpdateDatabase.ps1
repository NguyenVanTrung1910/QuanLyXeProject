$MigrationName = Read-Host "Enter migration name"


Chạy đoạn mã này trong CMS
dotnet ef migrations add ThemMoi1 --project ../Infrastructure/Infrastructure.csproj
dotnet ef database update --project ../Infrastructure/Infrastructure.csproj


Read-Host -Prompt "Press Enter to exit"
