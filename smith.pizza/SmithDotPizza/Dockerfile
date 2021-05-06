FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG configuration=Debug
WORKDIR /SmithDotPizza
COPY *.csproj .
RUN dotnet restore
COPY . ./
RUN dotnet publish -c $configuration -o artifacts

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /SmithDotPizza
COPY --from=build /SmithDotPizza/artifacts .
ENTRYPOINT [ "dotnet", "SmithDotPizza.dll" ]
