using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.ContactForms;
using Dhoojol.Application.Services.ContactForms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhoojol.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("contactforms")]
    public class ContactFormsController : Controller
    {
        private readonly IContactFormsService _contactFormsService;

        public ContactFormsController(IContactFormsService contactFormsService)
        {
            _contactFormsService = contactFormsService;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetContactFormModel>>> GetAllContactForms()
        {
            List<GetContactFormModel> allContactForms = await _contactFormsService.GetAllContactForms();
            return Ok(allContactForms);
        }

        [HttpPost("user")]
        [Authorize]
        public async Task<ActionResult<ApiResult<Guid>>> CreateUserContactForm([FromBody] CreateContactFormModel model)
        {
            try
            {
                Guid contactFormId = await _contactFormsService.CreateUserContactForm(model);
                return Ok(ApiResult.Success(contactFormId));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResult.Failed<Guid>(ex.Message));
            }
        }
        [HttpPost("guest")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResult<Guid>>> CreateGuestContactForm([FromBody] CreateContactFormModel model)
        {
            try
            {
                Guid contactFormId = await _contactFormsService.CreateGuestContactForm(model);
                return Ok(ApiResult.Success(contactFormId));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResult.Failed<Guid>(ex.Message));
            }
        }
    }
}
