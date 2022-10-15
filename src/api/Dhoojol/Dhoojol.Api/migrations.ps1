$Migration = Read-Host -Prompt 'Input your Migration name'
dotnet ef migrations add $Migration -s ./Dhoojol.Api.csproj -p ../Dhoojol.Infrastructure/Dhoojol.Infrastructure.csproj --context DhoojolContext -v
pause