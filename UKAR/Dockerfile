FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["UKAR/UKAR.csproj", "UKAR/"]
RUN dotnet restore "UKAR/UKAR.csproj"
COPY . .
WORKDIR "/src/UKAR"
RUN dotnet build "UKAR.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UKAR.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UKAR.dll"]