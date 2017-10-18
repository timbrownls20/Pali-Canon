

dotnet add package MongoDB.Driver
dotnet add package HtmlAgilityPack

dotnet new console -o PaliCanon.Loader
dotnet new classlib -o PaliCanon.Common
dotnet add reference ../PaliCanon.Common/PaliCanon.Common.csproj

dotnet new webapi -o PaliCanon.Api
dotnet add reference ../PaliCanon.Common/PaliCanon.Common.csproj

dotnet new sln -o PaliCanon
dotnet sln add PaliCanon.Loader/PaliCanon.Loader.csproj
dotnet sln add PaliCanon.Api/PaliCanon.Api.csproj