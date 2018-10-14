FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 53670
EXPOSE 44310

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["NoteApi/NoteApi.csproj", "NoteApi/"]
RUN dotnet restore "NoteApi/NoteApi.csproj"
COPY . .
WORKDIR "/src/NoteApi"
RUN dotnet build "NoteApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NoteApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NoteApi.dll"]