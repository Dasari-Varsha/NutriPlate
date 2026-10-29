namespace NutriPlate.Api.Configuration;
public class MongoDbSettings { public string ConnectionString {get;set;} = "mongodb://127.0.0.1:27017"; public string DatabaseName {get;set;} = "NutriPlate"; }
public class JwtSettings { public string Key {get;set;} = ""; public string Issuer {get;set;} = "NutriPlate.Api"; public string Audience {get;set;} = "NutriPlate.Client"; }
