FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["OrderProducerService.Api/OrderProducerService.Api.csproj", "OrderProducerService.Api/"]
COPY ["OrderProducerService.Application/OrderProducerService.Application.csproj", "OrderProducerService.Application/"]
COPY ["OrderProducerService.Domain/OrderProducerService.Domain.csproj", "OrderProducerService.Domain/"]
COPY ["OrderProducerService.Infrastructure/OrderProducerService.Infrastructure.csproj", "OrderProducerService.Infrastructure/"]
RUN dotnet restore "OrderProducerService.Api/OrderProducerService.Api.csproj"
COPY . .
WORKDIR "/src/OrderProducerService.Api"
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "OrderProducerService.Api.dll"]
