using Microsoft.AspNetCore.Mvc;using Microsoft.AspNetCore.Authorization;using System.Security.Claims;using NutriPlate.Api.Services;using NutriPlate.Api.DTOs;
namespace NutriPlate.Api.Controllers;
[ApiController][Authorize][Route("api/users")]
public class UsersController(NutriPlateService service):ControllerBase {
 private string Uid=>User.FindFirstValue(ClaimTypes.NameIdentifier)!;
 [HttpGet("me")] public async Task<IActionResult> Me(CancellationToken ct){var u=await service.GetUser(Uid,ct);return u==null?NotFound():Ok(new{id=u.Id,email=u.Email,displayName=u.DisplayName,dietaryPreferences=u.DietaryPreferences,foodRestrictions=u.FoodRestrictions});}
 [HttpPut("me")] public async Task<IActionResult> Update(UpdateProfileRequest req,CancellationToken ct){var u=await service.UpdateProfile(Uid,req.DisplayName,req.DietaryPreferences,req.FoodRestrictions,ct);return Ok(new{id=u.Id,email=u.Email,displayName=u.DisplayName,dietaryPreferences=u.DietaryPreferences,foodRestrictions=u.FoodRestrictions});}
}
