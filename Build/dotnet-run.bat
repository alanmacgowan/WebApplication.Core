

cd ..\WebApplication.Core.API
start /d "." dotnet run

cd  ..\WebApplication.Core.UI
Set "WebApplicationAPIUrl=http://localhost:5000"
start /d "." dotnet run
npm run production