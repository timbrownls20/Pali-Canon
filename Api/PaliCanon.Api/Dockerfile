#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PaliCanon.Api/PaliCanon.Api.csproj", "PaliCanon.Api/"]
RUN dotnet restore "PaliCanon.Api/PaliCanon.Api.csproj"
COPY . .
WORKDIR "/src/PaliCanon.Api"
RUN dotnet build "PaliCanon.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaliCanon.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaliCanon.Api.dll"]