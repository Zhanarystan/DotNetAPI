FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY DotNetAPI.csproj DotNetAPI.csproj
RUN dotnet restore "DotNetAPI.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "DotNetAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetAPI.dll"]

