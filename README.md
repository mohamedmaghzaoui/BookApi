# install packages
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet tool install --global dotnet-ef
# do migrations
dotnet ef database update
# run the app
dotnet run
app well be on http://localhost:5163

# group
Ramla Argui and Mohamed Maghzaoui