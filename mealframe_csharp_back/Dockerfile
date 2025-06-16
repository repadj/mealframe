FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj files in dependency order
COPY *.sln .
COPY Base.BLL.Contracts/*.csproj ./Base.BLL.Contracts/
COPY Base.Contracts/*.csproj ./Base.Contracts/
COPY Base.DAL.Contracts/*.csproj ./Base.DAL.Contracts/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.Helpers/*.csproj ./Base.Helpers/
COPY Base.Resources/*.csproj ./Base.Resources/
COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.DAL.EF/*.csproj ./Base.DAL.EF/

COPY App.BLL.Contracts/*.csproj ./App.BLL.Contracts/
COPY App.DAL.Contracts/*.csproj ./App.DAL.Contracts/
COPY App.DTO/*.csproj ./App.DTO/
COPY App.Domain/*.csproj ./App.Domain/
COPY App.Resources/*.csproj ./App.Resources/
COPY App.BLL/*.csproj ./App.BLL/
COPY App.DAL.DTO/*.csproj ./App.DAL.DTO/
COPY App.DAL.EF/*.csproj ./App.DAL.EF/
COPY APP.BLL.DTO/*.csproj ./APP.BLL.DTO/
COPY App.Tests/*.csproj ./App.Tests/
COPY WebApp/*.csproj ./WebApp/

# Restore
RUN dotnet restore

# Copy source files in the same order
COPY Base.BLL.Contracts/. ./Base.BLL.Contracts/
COPY Base.Contracts/. ./Base.Contracts/
COPY Base.DAL.Contracts/. ./Base.DAL.Contracts/
COPY Base.Domain/. ./Base.Domain/
COPY Base.Helpers/. ./Base.Helpers/
COPY Base.Resources/. ./Base.Resources/
COPY Base.BLL/. ./Base.BLL/
COPY Base.DAL.EF/. ./Base.DAL.EF/

COPY App.BLL.Contracts/. ./App.BLL.Contracts/
COPY App.DAL.Contracts/. ./App.DAL.Contracts/
COPY App.DTO/. ./App.DTO/
COPY App.Domain/. ./App.Domain/
COPY App.Resources/. ./App.Resources/
COPY App.BLL/. ./App.BLL/
COPY App.DAL.DTO/. ./App.DAL.DTO/
COPY App.DAL.EF/. ./App.DAL.EF/
COPY APP.BLL.DTO/. ./APP.BLL.DTO/
COPY App.Tests/. ./App.Tests/
COPY WebApp/. ./WebApp/

RUN dotnet publish -c Release -o out

#for intel cpu: aspnet:9.0-bookworm-slim-amd64 AS runtime
FROM mcr.microsoft.com/dotnet/aspnet:latest AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 8080
ENV ConnectionStrings:SqlServerConnection="Host=host.docker.internal;Port=5432;Database=postgres;Username=postgres;Password=postgres"
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "WebApp.dll"]