FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MatchManager.sln ./
COPY MatchAppUI/MatchAppUI.csproj MatchAppUI/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/MatchAppUI
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MatchAppUI.dll"]
