using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.Sessions;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Sessions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhoojol.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("sessions")]
    public class SessionsController : Controller
    {
        private readonly ISessionsService _sessionsService;

        public SessionsController(ISessionsService sessionsService)
        {
            _sessionsService = sessionsService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<ListSessionModel>>>> GetAllAsync()
        {
            List<ListSessionModel> sessionList = await _sessionsService.GetAllAsync();
            return Ok(sessionList);
        }

        [Authorize(Roles = "client")]
        [HttpGet("client")]
        public async Task<ActionResult<ApiResult<List<ListSessionModel>>>> GetClientSessions()
        {
            List<ListSessionModel> sessionList = await _sessionsService.GetClientSessions();
            return Ok(sessionList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<GetSessionModel>>> GetById(Guid id)
        {
            GetSessionModel session = await _sessionsService.GetById(id);
            return Ok(ApiResult.Success(session));
        }

        [HttpGet("{id}/coach")]
        public async Task<ActionResult<ApiResult<ListSessionModel>>> GetByCoachId([FromRoute] Guid id)
        {
            List<ListSessionModel> coachSessions = await _sessionsService.GetByCoachUserId(id);
            return Ok(ApiResult.Success(coachSessions));
        }

        [Authorize(Roles = "coach")]
        [HttpGet("{id}/participants")]
        public async Task<ActionResult<ApiResult<List<GetParticipantModel>>>> GetParticipants(Guid id)
        {
            try
            {
                List<GetParticipantModel> participantList = await _sessionsService.GetParticipants(id);
                return Ok(ApiResult.Success(participantList));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "client")]
        [HttpPost("{id}")]
        public async Task<ActionResult> JoinSession([FromRoute] Guid id)
        {
            try { 
                await _sessionsService.JoinSession(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "coach")]
        [HttpPost]
        public async Task<ActionResult<ApiResult<Guid>>> CreateSession([FromBody] CreateSessionModel model)
        {
            Guid sessionId = await _sessionsService.CreateSession(model);
            return Ok(ApiResult.Success(sessionId));
        }

        [Authorize(Roles = "coach")]
        [HttpPut]
        public async Task<ActionResult> UpdateSession(UpdateSessionModel model)
        {
            try
            {
                await _sessionsService.UpdateSession(model);
                return Ok(ApiResult.Success(model.Id));
            }

            catch (Exception ex)
            {
                return BadRequest(ApiResult.Failed<Guid>(ex.Message));
            }
        }

        [Authorize(Roles = "coach")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession([FromRoute] Guid id)
        {
            try
            {
                await _sessionsService.DeleteSession(id);
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ApiResult.Failed<Guid>(ex.Message));
            }
        }
    }
}
