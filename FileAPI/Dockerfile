FROM microsoft/aspnetcore:2.0-stretch AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-stretch AS build
WORKDIR /src
COPY FileApi/FileApi.csproj FileApi/
RUN dotnet restore "FileApi/FileApi.csproj"
COPY . .
WORKDIR "/src/FileApi"
RUN dotnet build "FileApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileApi.dll"]
