# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY PrimeTech.Core/*.csproj PrimeTech.Core/
COPY PrimeTech.Data/*.csproj PrimeTech.Data/
COPY PrimeTech.Api/*.csproj PrimeTech.Api/
COPY PrimeTech.Tests/*.csproj PrimeTech.Tests/
RUN dotnet restore
COPY . .

# testing
FROM build AS testing
WORKDIR /src/PrimeTech.Api
RUN dotnet build
WORKDIR /src/PrimeTech.Tests
RUN dotnet test

# publish
FROM build AS publish
WORKDIR /src/PrimeTech.Api
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
# ENTRYPOINT ["dotnet", "PrimeTech.Api.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet PrimeTech.Api.dll