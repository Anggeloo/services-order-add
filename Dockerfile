# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /serviceorderadd

EXPOSE 81
EXPOSE 5001

COPY ./*.csproj ./
RUN dotnet restore 

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0 
WORKDIR /serviceorderadd
COPY --from=build /serviceorderadd/out .
ENTRYPOINT ["dotnet", "services-order-add.dll"]
