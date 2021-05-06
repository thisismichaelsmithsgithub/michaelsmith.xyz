FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /SmithDotPizza
COPY *.csproj .
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o artifacts

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /SmithDotPizza
COPY --from=build /SmithDotPizza/artifacts .
ENTRYPOINT [ "dotnet", "SmithDotPizza.dll" ]
