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
    public async Task<ActionResult<ServiceResponse<GetCoachModel>>> GetCoach(Guid id)
    {
        try
        {
            var coach = await _coachesService.GetCoachByUserId(id);
            return Ok(ServiceResponse.Success(coach));
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse.Failed(ex.Message));
        }
    }


    [HttpPut]
    public async Task<IActionResult> UpdateCoach(GetCoachModelWithUserId model)
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
