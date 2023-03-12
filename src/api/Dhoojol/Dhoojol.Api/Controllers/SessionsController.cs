using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.Sessions;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace Dhoojol.Api.Controllers
{
    [ApiController]
    [Route("sessions")]
    public class SessionsController : Controller
    {
        private readonly ISessionsService _sessionsService;

        public SessionsController(ISessionsService sessionsService)
        {
            _sessionsService = sessionsService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<ListSessionModel>>>> GetAllAsync()
        {
            List<ListSessionModel> sessionList = await _sessionsService.GetAllAsync();
            return Ok(sessionList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<GetSessionModel>>> GetById(Guid id)
        {
            GetSessionModel session = await _sessionsService.GetById(id);
            return Ok(ApiResult.Success(session));
        }
        [HttpGet("{id}/participants")]
        public async Task<ActionResult<ApiResult<List<GetParticipantModel>>>> GetParticipants(Guid id)
        {
            List<GetParticipantModel> participantList = await _sessionsService.GetParticipants(id);
            return Ok(ApiResult.Success(participantList));
        }
    }
}
