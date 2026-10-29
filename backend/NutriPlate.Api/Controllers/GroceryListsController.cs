using Microsoft.AspNetCore.Mvc;using Microsoft.AspNetCore.Authorization;using System.Security.Claims;using NutriPlate.Api.Services;using NutriPlate.Api.DTOs;
namespace NutriPlate.Api.Controllers;
[ApiController][Authorize][Route("api/grocerylists")]
public class GroceryListsController(NutriPlateService service):ControllerBase {
 private string Uid=>User.FindFirstValue(ClaimTypes.NameIdentifier)!;
 [HttpGet] public async Task<IActionResult> List(CancellationToken ct)=>Ok(await service.GetGroceries(Uid,ct));
 [HttpPost("generate")] public async Task<IActionResult> Generate(GenerateGroceryRequest r,CancellationToken ct)=>Ok(await service.GenerateGroceries(Uid,r.MealPlanId,ct));
 [HttpPost("{id}/items")] public async Task<IActionResult> Add(string id,AddGroceryItemRequest r,CancellationToken ct)=>Ok(await service.AddGrocery(Uid,id,r.Name,r.Quantity,r.Unit,ct));
 [HttpPatch("{id}/items/{itemId}")] public async Task<IActionResult> Toggle(string id,string itemId,ToggleGroceryRequest r,CancellationToken ct)=>Ok(await service.ToggleGrocery(Uid,id,itemId,r.Purchased,ct));
 [HttpDelete("{id}/items/{itemId}")] public async Task<IActionResult> Remove(string id,string itemId,CancellationToken ct){await service.RemoveGrocery(Uid,id,itemId,ct);return NoContent();}
 [HttpDelete("{id}")] public async Task<IActionResult> Delete(string id,CancellationToken ct){await service.DeleteGrocery(Uid,id,ct);return NoContent();}
}
