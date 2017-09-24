FROM microsoft/dotnet as builder

WORKDIR /dotnetapp

COPY *.csproj ./
RUN dotnet restore

COPY . .

RUN dotnet build

FROM microsoft/dotnet:runtime

WORKDIR /dotnetapp
COPY --from=builder /dotnetapp/bin/Debug/netcoreapp2.0 .
ENTRYPOINT ["dotnet", "searchfight.dll"]