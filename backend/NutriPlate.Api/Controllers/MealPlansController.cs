using Microsoft.AspNetCore.Mvc;using Microsoft.AspNetCore.Authorization;using System.Security.Claims;using NutriPlate.Api.Services;using NutriPlate.Api.DTOs;
namespace NutriPlate.Api.Controllers;
[ApiController][Authorize][Route("api/mealplans")]
public class MealPlansController(NutriPlateService service):ControllerBase {
 private string Uid=>User.FindFirstValue(ClaimTypes.NameIdentifier)!;
 [HttpGet] public async Task<IActionResult> List(CancellationToken ct)=>Ok(await service.GetPlans(Uid,ct));
 [HttpPost] public async Task<IActionResult> Create(CreatePlanRequest r,CancellationToken ct){var p=await service.CreatePlan(Uid,r.Name,r.StartDate,r.EndDate,ct);return CreatedAtAction(nameof(List),new{id=p.Id},p);}
 [HttpGet("{id}")] public async Task<IActionResult> Get(string id,CancellationToken ct){var p=(await service.GetPlans(Uid,ct)).FirstOrDefault(x=>x.Id==id);return p==null?NotFound():Ok(p);}
 [HttpPost("{id}/meals")] public async Task<IActionResult> Add(string id,AddMealRequest r,CancellationToken ct)=>Ok(await service.AddMeal(Uid,id,r.RecipeId,r.Date,r.MealType,r.Servings,ct));
 [HttpDelete("{id}/meals/{mealId}")] public async Task<IActionResult> Remove(string id,string mealId,CancellationToken ct){await service.RemoveMeal(Uid,id,mealId,ct);return NoContent();}
 [HttpDelete("{id}")] public async Task<IActionResult> Delete(string id,CancellationToken ct){var p=(await service.GetPlans(Uid,ct)).FirstOrDefault(x=>x.Id==id);if(p==null)return NotFound();await service.DeletePlan(Uid,id,ct);return NoContent();}
}
