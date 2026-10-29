using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace NutriPlate.Api.Models;
public class Nutrition { public double Calories {get;set;} public double Protein {get;set;} public double Carbohydrates {get;set;} public double Fat {get;set;} }
public class Ingredient { public string Name {get;set;} = ""; public double Quantity {get;set;} public string Unit {get;set;} = ""; }
public class Recipe {
 [BsonId] [BsonRepresentation(BsonType.ObjectId)] public string? Id {get;set;}
 public string Name {get;set;} = ""; public string Description {get;set;} = ""; public List<Ingredient> Ingredients {get;set;} = [];
 public List<string> Instructions {get;set;} = []; public int PrepMinutes {get;set;} public int Servings {get;set;} = 1;
 public string Difficulty {get;set;} = "Easy"; public string Cuisine {get;set;} = ""; public List<string> DietaryTags {get;set;} = [];
 public Nutrition Nutrition {get;set;} = new();
}
public class AppUser {
 [BsonId] [BsonRepresentation(BsonType.ObjectId)] public string? Id {get;set;}
 public string Email {get;set;} = ""; public string DisplayName {get;set;} = ""; public string PasswordHash {get;set;} = "";
 public List<string> DietaryPreferences {get;set;} = []; public List<string> FoodRestrictions {get;set;} = [];
 public List<string> FavoriteRecipeIds {get;set;} = []; public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
}
public class PlannedMeal {
 public string Id {get;set;} = Guid.NewGuid().ToString("N"); public string RecipeId {get;set;} = ""; public string RecipeName {get;set;} = "";
 public string Date {get;set;} = ""; public string MealType {get;set;} = "Lunch"; public int Servings {get;set;} = 1; public Nutrition Nutrition {get;set;} = new();
}
public class MealPlan {
 [BsonId] [BsonRepresentation(BsonType.ObjectId)] public string? Id {get;set;} public string UserId {get;set;} = "";
 public string Name {get;set;} = ""; public string StartDate {get;set;} = ""; public string EndDate {get;set;} = ""; public List<PlannedMeal> Meals {get;set;} = [];
}
public class GroceryItem { public string Id {get;set;} = Guid.NewGuid().ToString("N"); public string Name {get;set;} = ""; public double Quantity {get;set;} public string Unit {get;set;} = "item"; public bool Purchased {get;set;} }
public class GroceryList {
 [BsonId] [BsonRepresentation(BsonType.ObjectId)] public string? Id {get;set;} public string UserId {get;set;} = "";
 public string Name {get;set;} = ""; public string MealPlanId {get;set;} = ""; public List<GroceryItem> Items {get;set;} = []; public DateTime UpdatedAt {get;set;} = DateTime.UtcNow;
}
