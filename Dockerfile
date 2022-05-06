#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JiraPoker/JiraPoker.csproj", "JiraPoker/"]
COPY ["JiraPoker.Core/JiraPoker.Core.csproj", "JiraPoker.Core/"]
RUN dotnet restore "JiraPoker/JiraPoker.csproj"
COPY . .
WORKDIR "/src/JiraPoker"
RUN dotnet build "JiraPoker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JiraPoker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#COPY ./JiraPoker/wwwroot ./wwwroot
#ENTRYPOINT ["dotnet", "JiraPoker.dll"]

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet JiraPoker.dll