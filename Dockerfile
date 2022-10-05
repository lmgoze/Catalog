#Each line represents a set of instructions to be applied as the docker image to be built
#Each of these line will also generate a LAYER that represents the changes that are happening from one line to the next line.

#built by dotnet asp net image, run on a linux machine
#AS base means first stage of the building the container
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
#dotnet/sdk has all the build tools & libraries to build the container
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Catalog.csproj", "./"]
RUN dotnet restore "Catalog.csproj"
COPY . .
#publish creates a new folder, publish, with all the files needed to execute the app
RUN dotnet publish "Catalog.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Catalog.dll"]
