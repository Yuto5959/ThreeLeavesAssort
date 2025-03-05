# ビルドステージ
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
RUN apt-get update && apt-get install -y locales
RUN echo "en_US.UTF-8 UTF-8" >> /etc/locale.gen && locale-gen
ENV LANG=en_US.UTF-8
ENV LC_ALL=en_US.UTF-8
WORKDIR /app
COPY . ./
COPY ["wwwroot/", "/app/wwwroot/"]
RUN dotnet restore
RUN dotnet publish -c Release -o out

# ランタイムステージ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "ThreeLeavesAssort.dll"]