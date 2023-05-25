using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Business;
using Focus.Domain.Interface;
using Focus.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using Focus.Business.Components;
using Microsoft.AspNet.Identity;
using Noble.Api.Models;
using System.Threading.Tasks;
using Focus.Business.Benificary.Models;
using DocumentFormat.OpenXml.Bibliography;
using Focus.Business.Benificary.Commands;
using Focus.Business.Benificary.Queries;
using System;

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
    }
}
