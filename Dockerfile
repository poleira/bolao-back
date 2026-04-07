FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BolaoDaCopa/BolaoDaCopa.csproj", "BolaoDaCopa/"]
RUN dotnet restore "BolaoDaCopa/BolaoDaCopa.csproj"
COPY . .
WORKDIR "/src/BolaoDaCopa"
RUN dotnet build "BolaoDaCopa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BolaoDaCopa.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BolaoDaCopa.dll"]
