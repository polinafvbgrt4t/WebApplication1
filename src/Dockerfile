
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /repos


COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]
COPY ["Console_API/Console_API.csproj", "ConsoleAPI/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]


RUN dotnet restore "WebApplication1/WebApplication1.csproj"


COPY . .

FROM build AS publish
RUN dotnet publish "WebApplication1/WebApplication1.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]