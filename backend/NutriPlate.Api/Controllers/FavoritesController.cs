using Microsoft.AspNetCore.Mvc;using Microsoft.AspNetCore.Authorization;using System.Security.Claims;using NutriPlate.Api.Services;
namespace NutriPlate.Api.Controllers;
[ApiController][Authorize][Route("api/favorites")]
public class FavoritesController(NutriPlateService service):ControllerBase {
 private string Uid=>User.FindFirstValue(ClaimTypes.NameIdentifier)!;
 [HttpGet] public async Task<IActionResult> List(CancellationToken ct)=>Ok(await service.Favorites(Uid,ct));
 [HttpPost("{recipeId}")] public async Task<IActionResult> Add(string recipeId,CancellationToken ct){await service.SetFavorite(Uid,recipeId,true,ct);return NoContent();}
 [HttpDelete("{recipeId}")] public async Task<IActionResult> Remove(string recipeId,CancellationToken ct){await service.SetFavorite(Uid,recipeId,false,ct);return NoContent();}
}
