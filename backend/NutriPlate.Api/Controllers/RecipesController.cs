using Microsoft.AspNetCore.Mvc;using Microsoft.AspNetCore.Authorization;using System.Security.Claims;using NutriPlate.Api.Services;using NutriPlate.Api.Models;using MongoDB.Driver;using MongoDB.Bson;
namespace NutriPlate.Api.Controllers;
[ApiController][Route("api/recipes")]
public class RecipesController(NutriPlateService service,IMongoDatabase db):ControllerBase {
 [HttpGet] public async Task<IActionResult> List([FromQuery]string? search,[FromQuery]int page=1,[FromQuery]int pageSize=20,CancellationToken ct=default){page=Math.Max(1,page);pageSize=Math.Clamp(pageSize,1,100);var all=await service.ListRecipes(search,ct);return Ok(all.Skip((page-1)*pageSize).Take(pageSize));}
 [HttpGet("{id}")] public async Task<IActionResult> Get(string id,CancellationToken ct){var r=await service.GetRecipe(id,ct);return r==null?NotFound(new{error="Recipe not found."}):Ok(r);}
 [Authorize][HttpPost] public async Task<IActionResult> Create(Recipe recipe,CancellationToken ct){recipe.Id=ObjectId.GenerateNewId().ToString();if(string.IsNullOrWhiteSpace(recipe.Name)||recipe.Servings<1||recipe.PrepMinutes<0)return BadRequest(new{error="Name, positive servings, and non-negative prep time are required."});await db.GetCollection<Recipe>("Recipes").InsertOneAsync(recipe,cancellationToken:ct);return CreatedAtAction(nameof(Get),new{id=recipe.Id},recipe);}
 [Authorize][HttpPut("{id}")] public async Task<IActionResult> Update(string id,Recipe recipe,CancellationToken ct){if(!ObjectId.TryParse(id,out _))return NotFound();recipe.Id=id;var result=await db.GetCollection<Recipe>("Recipes").ReplaceOneAsync(x=>x.Id==id,recipe,cancellationToken:ct);return result.MatchedCount==0?NotFound():Ok(recipe);}
 [Authorize][HttpDelete("{id}")] public async Task<IActionResult> Delete(string id,CancellationToken ct){var result=await db.GetCollection<Recipe>("Recipes").DeleteOneAsync(x=>x.Id==id,ct);return result.DeletedCount==0?NotFound():NoContent();}
}
