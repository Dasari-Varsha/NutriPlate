using Xunit;
namespace NutriPlate.Tests;
public class NutritionTests {
 public record Nutrition(double Calories,double Protein,double Carbohydrates,double Fat);
 public static Nutrition Scale(Nutrition n,int servings)=>new(n.Calories*servings,n.Protein*servings,n.Carbohydrates*servings,n.Fat*servings);
 [Fact] public void ServingScalingMultipliesEachMacro(){var result=Scale(new(200,10,30,5),3);Assert.Equal(600,result.Calories);Assert.Equal(30,result.Protein);Assert.Equal(90,result.Carbohydrates);Assert.Equal(15,result.Fat);}
 [Fact] public void ZeroServingsProducesZeroTotals(){var result=Scale(new(200,10,30,5),0);Assert.Equal(0,result.Calories);Assert.Equal(0,result.Protein);}
 [Theory][InlineData("Breakfast",true)][InlineData("Lunch",true)][InlineData("Supper",false)][InlineData("Snack",true)] public void MealTypeAllowList(string type,bool expected)=>Assert.Equal(expected,new[]{"Breakfast","Lunch","Dinner","Snack"}.Contains(type));
}
