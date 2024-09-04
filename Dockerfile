FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ASPShopAPI/*.csproj ./ASPShopAPI/
RUN dotnet restore

# copy everything else and build app
COPY ASPShopAPI/. ./ASPShopAPI/
WORKDIR /source/ASPShopAPI
RUN dotnet build -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5070

ENTRYPOINT ["dotnet", "ASPShopAPI.dll"]
