# 1. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy tất cả các file .csproj để restore NuGet (Tối ưu hóa cache)
COPY ["API_ND/API_ND.csproj", "API_ND/"]
COPY ["BLL/BLL.csproj", "BLL/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["Models/Models.csproj", "Models/"]
COPY ["Services/Services.csproj", "Services/"]

# Thực hiện restore cho dự án chính (nó sẽ tự kéo theo các dự án phụ)
RUN dotnet restore "API_ND/API_ND.csproj"

# Copy toàn bộ mã nguồn của tất cả các thư mục vào container
COPY . .

# Build dự án
WORKDIR "/src/API_ND"
RUN dotnet build "API_ND.csproj" -c Release -o /app/build

# 2. Publish Stage
FROM build AS publish
RUN dotnet publish "API_ND.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3. Final Stage (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Render thường sử dụng port 10000 hoặc tùy biến theo biến môi trường PORT
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "API_ND.dll"]