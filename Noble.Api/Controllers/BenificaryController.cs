using Focus.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Focus.Business.Benificary.Models;
using Focus.Business.Benificary.Commands;
using Focus.Business.Benificary.Queries;
using System;
using Focus.Business.AuthorizPersons.Model;
using Focus.Business.AuthorizPersons.Commands;
using Focus.Business.AuthorizPersons.Queries;
using Focus.Business.BenificiariesNotes.Model;
using Focus.Business.BenificiariesNotes.Commands;
using Focus.Business.BenificiariesNotes.Queries;

namespace Noble.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BenificaryController : BaseController
    {
        public readonly ApplicationDbContext _Context;

        public BenificaryController(ApplicationDbContext context)
        {
            _Context = context;        
        }

        #region Benificary
        [Route("api/Benificary/SaveBenificary")]
        [HttpPost("SaveBenificary")]
        public async Task<IActionResult> SaveBenificary([FromBody] BenificariesLookupModel benificaries)
        {
            var message = await Mediator.Send(new BenificiariesAddUpdateCommand
            {
                benificiaries = benificaries
            });
            return Ok(message);
        }
        [Route("api/Benificary/GetBenificaryList")]
        [HttpGet("GetBenificaryList")]
        public async Task<IActionResult> GetBenificaryList(string searchTerm, int? pageNumber, bool isDropDown)
        {
            var benificary = await Mediator.Send(new GetBenificariesListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1
            });
            return Ok(benificary);
        }

        [Route("api/Benificary/GetBenificaryDetail")]
        [HttpGet("GetBenificaryDetail")]
        public async Task<IActionResult> GetBenificaryDetail(Guid id)
        {
            var benificary = await Mediator.Send(new GetBenificariesDetailsQuery
            {
                Id = id
            });
            return Ok(benificary);
        }

        #endregion

        #region AuthorizedPersons
        [Route("api/Benificary/SaveAuthorizedPersons")]
        [HttpPost("SaveAuthorizedPersons")]
        public async Task<IActionResult> SaveAuthorizedPersons([FromBody] AuthorizedPersonsLookupModel authorized)
        {
            var message = await Mediator.Send(new AuthorizedPersonsAddUpdateCommand
            {
                authorziedPersons = authorized
            });
            return Ok(message);
        }
        [Route("api/Benificary/GetAuthorizedPersonsList")]
        [HttpGet("GetAuthorizedPersonsList")]
        public async Task<IActionResult> GetAuthorizedPersonsList(string searchTerm, int? pageNumber, bool isDropDown)
        {
            var authorziedPersons = await Mediator.Send(new GetAuthorizedPersonListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1
            });
            return Ok(authorziedPersons);
        }

        [Route("api/Benificary/GetAuthorizedPersonsDetail")]
        [HttpGet("GetAuthorizedPersonsDetail")]
        public async Task<IActionResult> GetAuthorizedPersonsDetail(Guid id)
        {
            var authorziedPersons = await Mediator.Send(new GetAuthorizedPersonDetailsQuery
            {
                Id = id
            });
            return Ok(authorziedPersons);
        }

        #endregion

        #region BenificaryNote
        [Route("api/Benificary/SaveBenificaryNote")]
        [HttpPost("SaveBenificaryNote")]
        public async Task<IActionResult> SaveBenificaryNote([FromBody] BenificaryNoteLookupModel benificary)
        {
            var message = await Mediator.Send(new BenificaryNoteAddUpdateCommand
            {
                benificaryNote = benificary
            });
            return Ok(message);
        }
        [Route("api/Benificary/GetBenificaryNoteList")]
        [HttpGet("GetBenificaryNoteList")]
        public async Task<IActionResult> GetBenificaryNoteList(string searchTerm, int? pageNumber, bool isDropDown)
        {
            var benificary = await Mediator.Send(new GetBenificaryNoteListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1
            });
            return Ok(benificary);
        }

        [Route("api/Benificary/GetBenificaryNoteDetail")]
        [HttpGet("GetBenificaryNoteDetail")]
        public async Task<IActionResult> GetBenificaryNoteDetail(Guid id)
        {
            var benificary = await Mediator.Send(new GetBenificaryNoteDetailsQuery
            {
                Id = id
            });
            return Ok(benificary);
        }

        #endregion
    }
}
