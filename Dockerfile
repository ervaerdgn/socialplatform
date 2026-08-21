FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore socialplatform/socialplatform.csproj
RUN dotnet publish socialplatform/socialplatform.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["sh", "-c", "ASPNETCORE_URLS=http://+:$PORT dotnet socialplatform.dll"]