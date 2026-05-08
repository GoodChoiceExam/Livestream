FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY FitLife.Livestream.slnx .
COPY FitLife.Livestream.Api/FitLife.Livestream.Api.csproj FitLife.Livestream.Api/
COPY FitLife.Livestream.Tests/FitLife.Livestream.Tests.csproj FitLife.Livestream.Tests/
RUN dotnet restore FitLife.Livestream.slnx

COPY . .
RUN dotnet test FitLife.Livestream.Tests/FitLife.Livestream.Tests.csproj --no-restore
RUN dotnet publish FitLife.Livestream.Api/FitLife.Livestream.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FitLife.Livestream.Api.dll"]