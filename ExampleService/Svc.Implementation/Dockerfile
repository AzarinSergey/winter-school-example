FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
COPY ["bin/Debug/netcoreapp3.0", "app/"]

EXPOSE 15008

ENTRYPOINT ["dotnet", "app/Svc.Implementation.dll"]
