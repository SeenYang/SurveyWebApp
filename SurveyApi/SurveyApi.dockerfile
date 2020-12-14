FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#Copy nuget.config to the root folder.
COPY nuget.config .
#Copy all the content from the root folder to the ./app folder (including folder structure)
COPY . ./app

#Set workdir to the project folder (so all the commands from now on will automatically run inside the workdir folder)
WORKDIR /app/SurveyApi
RUN dotnet restore --ignore-failed-sources

WORKDIR /app
RUN dotnet test

WORKDIR /app/SurveyApi
RUN dotnet publish -c Release -o ../out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
ENV ASPNETCORE_URLS="http://*:80"
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "SurveyApi.dll"]