FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /src
#COPY RboWeb/RboWeb.csproj RboWeb/
#RUN dotnet restore "RboWeb/RboWeb.csproj"

#COPY Rbo/Rbo.csproj Rbo/
#RUN dotnet restore "Rbo/Rbo.csproj"

COPY . . 

RUN dotnet build -c Release -o /app/build

FROM build as publish
RUN dotnet publish "RboWeb/RboWeb.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RboWeb.dll"]