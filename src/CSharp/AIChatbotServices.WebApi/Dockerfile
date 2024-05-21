FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY . .
WORKDIR src/CSharp
RUN dotnet restore "AIChatbotServices.WebApi/AIChatbotServices.WebApi.csproj"
WORKDIR "/src/CSharp"
RUN dotnet build "AIChatbotServices.sln" -c Release -o /app/build

WORKDIR "/src/CSharp/AIChatbotServices.WebApi"
RUN dotnet build "AIChatbotServices.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AIChatbotServices.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AIChatbotServices.WebApi.dll"]