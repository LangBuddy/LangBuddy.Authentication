#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LangBuddy.Authentication.Web/LangBuddy.Authentication.Web.csproj", "LangBuddy.Authentication.Web/"]
COPY ["LangBuddy.Authentication.Models/LangBuddy.Authentication.Models.csproj", "LangBuddy.Authentication.Models/"]
COPY ["LangBuddy.Authentication.Service/LangBuddy.Authentication.Service.csproj", "LangBuddy.Authentication.Service/"]
COPY ["LangBuddy.Authentication.Database/LangBuddy.Authentication.Database.csproj", "LangBuddy.Authentication.Database/"]
RUN dotnet restore "LangBuddy.Authentication.Web/LangBuddy.Authentication.Web.csproj"
COPY . .
WORKDIR "/src/LangBuddy.Authentication.Web"
RUN dotnet build "LangBuddy.Authentication.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LangBuddy.Authentication.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LangBuddy.Authentication.Web.dll"]