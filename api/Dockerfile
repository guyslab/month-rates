FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MonthRates/MonthRates.csproj", "MonthRates/"]
COPY ["Lib/Lib.csproj", "Lib/"]
RUN dotnet restore "MonthRates/MonthRates.csproj"
COPY . .
WORKDIR "/src/MonthRates"
RUN dotnet build "MonthRates.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonthRates.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonthRates.dll"]