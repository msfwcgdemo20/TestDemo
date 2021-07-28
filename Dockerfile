FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
COPY nuget.config ./
COPY TestDemo.csproj ./TestDemo/

RUN dotnet restore TestDemo/TestDemo.csproj

# Copy everything else and build
COPY . ./TestDemo/

RUN dotnet publish TestDemo/TestDemo.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Development
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TestDemo.dll"]