FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["VendingMachine.csproj", "./"]
RUN dotnet restore "VendingMachine.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "VendingMachine.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VendingMachine.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VendingMachine.dll"] 