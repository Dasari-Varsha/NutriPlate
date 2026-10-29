using System.Text;using Microsoft.AspNetCore.Authentication.JwtBearer;using Microsoft.IdentityModel.Tokens;using MongoDB.Driver;using NutriPlate.Api.Configuration;using NutriPlate.Api.Services;using NutriPlate.Api.Middleware;
var builder=WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));
var mongoSettings=builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>()??new MongoDbSettings();
builder.Services.AddSingleton<IMongoClient>(_=>new MongoClient(mongoSettings.ConnectionString));
builder.Services.AddSingleton(sp=>sp.GetRequiredService<IMongoClient>().GetDatabase(mongoSettings.DatabaseName));
builder.Services.AddSingleton<NutriPlateService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();builder.Services.AddSwaggerGen();
var jwtKey=builder.Configuration["Jwt:Key"]??"";
if(jwtKey.Length>=32){builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o=>{o.TokenValidationParameters=new TokenValidationParameters{ValidateIssuer=true,ValidIssuer=builder.Configuration["Jwt:Issuer"]??"NutriPlate.Api",ValidateAudience=true,ValidAudience=builder.Configuration["Jwt:Audience"]??"NutriPlate.Client",ValidateLifetime=true,ValidateIssuerSigningKey=true,IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),ClockSkew=TimeSpan.FromSeconds(30)};});}
else {builder.Services.AddAuthentication();builder.Logging.AddConsole();}
builder.Services.AddAuthorization();
var origin=builder.Configuration["Frontend:Origin"]??"http://localhost:5173";
builder.Services.AddCors(o=>o.AddPolicy("Frontend",p=>p.WithOrigins(origin).AllowAnyHeader().AllowAnyMethod()));
var app=builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
if(app.Environment.IsDevelopment()){app.UseSwagger();app.UseSwaggerUI();}
app.UseCors("Frontend");app.UseAuthentication();app.UseAuthorization();app.MapControllers();
try{await app.Services.GetRequiredService<NutriPlateService>().EnsureIndexesAndSeed();app.Logger.LogInformation("NutriPlate indexes verified and seed check complete.");}catch(Exception ex){app.Logger.LogError(ex,"MongoDB initialization failed. Check MongoDb configuration and connection.");}
app.MapGet("/",()=>Results.Ok(new{application="NutriPlate API",status="running",docs="/swagger"}));
app.Run();
public partial class Program { }
