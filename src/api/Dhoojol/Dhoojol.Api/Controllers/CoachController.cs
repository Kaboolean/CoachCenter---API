using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.Coaches;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Coaches;
using Dhoojol.Application.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Dhoojol.Api.Controllers;

[ApiController]
[Route("coaches")]
public class CoachesController : Controller
{
    private readonly ICoachesService _coachesService;

    public CoachesController(ICoachesService coachesService)
    {
        _coachesService = coachesService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResult<GetCoachModel>>> GetCoach(Guid id)
    {
        try
        {
            var coach = await _coachesService.GetCoachByUserId(id);
            return Ok(ApiResult.Success(coach));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResult.Failed<GetCoachModel>(ex.Message));
        }
    }


    [HttpPut]
    public async Task<IActionResult> UpdateCoach(UpdateCoachModel model)
    {
        try
        {
            await _coachesService.UpdateCoach(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
