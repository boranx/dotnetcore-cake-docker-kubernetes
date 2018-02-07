FROM microsoft/aspnetcore:2.0.0
WORKDIR /app
COPY ./artifacts .
EXPOSE 80 443
ENTRYPOINT ["dotnet", "SampleWebApiAspNetCore.dll"]
