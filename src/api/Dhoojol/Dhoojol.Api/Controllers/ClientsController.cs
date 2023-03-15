using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Models.Clients;
using Dhoojol.Application.Models.Coaches;
using Dhoojol.Application.Services.Auth;
using Dhoojol.Application.Services.Clients;
using Dhoojol.Application.Services.Coaches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhoojol.Api.Controllers;

[ApiController]
[Authorize]
[Route("clients")]
public class ClientsController : Controller
{
    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResult<GetClientModel>>> GetClient(Guid id)
    {
        try
        {
            var client = await _clientsService.GetClientByUserId(id);
            return Ok(ApiResult.Success(client));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResult.Failed<GetClientModel>(ex.Message));
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateClient(UpdateClientModel model)
    {
        try
        {
            await _clientsService.UpdateClient(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
