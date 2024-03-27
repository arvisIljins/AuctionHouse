FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /app
EXPOSE 80

# copy all .csproj file and restore as ditinct layer and uze the same cpoy comand
COPY AuctionHouse.sln AuctionHouse.sln
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj
COPY src/BiddingService/BiddingService.csproj src/BiddingService/BiddingService.csproj
COPY src/AuctionService/AuctionService.csproj src/AuctionService/AuctionService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj
COPY tests/AuctionService.IntegrationTests/AuctionService.IntegrationTests.csproj tests/AuctionService.IntegrationTests/AuctionService.IntegrationTests.csproj 
COPY tests/SearchService.UnitTests/SearchService.UnitTests.csproj tests/SearchService.UnitTests/SearchService.UnitTests.csproj
COPY tests/AuctionService.UnitTests/AuctionService.UnitTests.csproj tests/AuctionService.UnitTests/AuctionService.UnitTests.csproj

# Restore package deps
RUN dotnet restore AuctionHouse.sln

# Copy the app folders over
COPY src/AuctionService src/AuctionService 
COPY src/Contracts src/Contracts 
WORKDIR /app/src/AuctionService
RUN dotnet publish -c Release -o /app/src/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "AuctionService.dll" ]