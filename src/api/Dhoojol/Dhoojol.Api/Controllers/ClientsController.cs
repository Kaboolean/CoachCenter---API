﻿using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Models.Clients;
using Dhoojol.Application.Services.Auth;
using Dhoojol.Application.Services.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Dhoojol.Api.Controllers;

[ApiController]
[Route("clients")]
public class ClientsController : Controller
{
    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetClientModel>>> GetClient(Guid id)
    {
        try
        {
            var client = await _clientsService.GetClient(id);
            return Ok(ServiceResponse.Success(client));
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse.Failed(ex.Message));
        }
    }
}
