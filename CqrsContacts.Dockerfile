FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /source

COPY . .

RUN dotnet restore

RUN dotnet publish -o /app -c release

FROM mcr.microsoft.com/dotnet/sdk:6.0 

WORKDIR /app

COPY --from=build /app . 

ENTRYPOINT [ "dotnet" , "CqrsContacts.Api.dll", "--urls", "http://+:5000", "-e" ,"ContactDatabase:ConnectionString=mongodb://mongodb:27017" ]