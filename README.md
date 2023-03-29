# ASP.NET-WebApiWithMongoDB
MongoDB를 사용한 ASP.NET Web Api 개발 방법을 정리해둔 페이지입니다.
## 개발 환경
- **IDE** : Visual Studio 2022
- **Framework** : ASP.NET Core 6
- **Database** : MongoDB Driver 2.19.1
## How to create project
### 1. 프로젝트 생성 및 패키지 관리자 콘솔에서 MongoDB 설치
- ASP.NET Core 웹 API를 선택하여 프로젝트 생성 (컨트롤러 사용 체크)
- MongoDB 설치
```
PM> Install-Package MongoDB.Driver
```
### 2. Entity Model 추가
- 해당 예제에서는 아래의 `Profile` Entity Model을 사용
```c#
public class Profile
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Server { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Level { get; set; }
}
```
### 3. Configuration Model 추가
- appsettings.json에 `ProfileDatabase` configuration 값을 추가
```json
"ProfileDatabase": {
  "ConnectionString": "mongodb://...",
  "DatabaseName": "...",
  "CollectionName": "..."
}
```
- `ProfileDatabaseSettings` 클래스 추가
  - 해당 클래스의 Property는 바로 전 추가한 configuration 값과 일치하도록 구현
```c#
// ProfileDatabaseSettings.cs
public class ProfileDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
}
```
- 종속성 주입 (Dependency Injection)
```c#
// Program.cs
builder.Services.Configure<ProfileDatabaseSettings>(
    builder.Configuration.GetSection("ProfileDatabase"));
```
### 4. CRUD Service 추가
- [ProfileService.cs](https://github.com/Wseop/ASP.NET-WebApiWithMongoDB/blob/main/WebApiMongoDB/Services/ProfileService.cs)
- 종속성 주입
> 공식 MongoDB Client Reuse Guideline에 따라 Singleton으로 등록되어야 함
```c#
// Program.cs
builder.Services.AddSingleton<ProfileService>();
```
### 5. Controller 추가
- [ProfileController.cs](https://github.com/Wseop/ASP.NET-WebApiWithMongoDB/blob/main/WebApiMongoDB/Controllers/ProfileController.cs)
## Swagger Middleware 추가
### 패키지 설치
- Swashbuckle.AspNetCore.Swagger
- Swashbuckle.AspNetCore.SwaggerGen
- Swashbuckle.AspNetCore.SwaggerUI
### 서비스 컬렉션에 추가
```c#
// Program.cs
builder.Services.AddSwaggerGen();
...
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```
### Swagger Index Page
![Swagger](https://github.com/Wseop/ASP.NET-WebApiWithMongoDB/blob/main/img/swagger_index.JPG)
