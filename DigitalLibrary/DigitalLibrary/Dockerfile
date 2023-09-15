FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DigitalLibrary/DigitalLibrary.csproj", "DigitalLibrary/"]
COPY ["Persistance/Persistance.csproj", "Persistance/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Service/Service.csproj", "Service/"]
RUN dotnet restore "DigitalLibrary/DigitalLibrary.csproj"
COPY . .
WORKDIR "/src/DigitalLibrary"
RUN dotnet build "DigitalLibrary.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DigitalLibrary.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DigitalLibrary.dll"]
