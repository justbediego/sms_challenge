FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5020
EXPOSE 5020

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SmsChallengeBackend.csproj", "."]
RUN dotnet restore "./SmsChallengeBackend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SmsChallengeBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmsChallengeBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmsChallengeBackend.dll"]