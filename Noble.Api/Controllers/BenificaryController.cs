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
using Focus.Business.CharityResource.Model;
using Focus.Business.CharityResource.Commands;
using Focus.Business.CharityResource.Queries;
using Focus.Business.ApprovalsPerson.Model;
using Focus.Business.ApprovalsPerson.Command;
using Focus.Business.ApprovalsPerson.Queries;
using Focus.Business.PaymentsType.Commands;
using Focus.Business.PaymentsType.Model;
using Focus.Business.PaymentsType.Queries;
using Focus.Business.AdminDashboard.Queries;
using Focus.Business.CharityFunds.Models;
using Focus.Business.CharityFunds.Commands;
using Focus.Business.CharityFunds.Queries;
using Focus.Business.Payments.Models;
using Focus.Business.Payments.Commands;
using Focus.Business.Payments.Queries;
using Focus.Business.Transactions.Queries;
using System.Collections.Generic;
using System.Linq;
using Focus.Domain.Entities;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using Noble.Api.Models;
using PaymentLookupModel = Focus.Business.Payments.Models.PaymentLookupModel;
using Org.BouncyCastle.Asn1.Ocsp;

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

        #region Dashboard
        [Route("api/Benificary/GetDashboardDetail")]
        [HttpGet("GetDashboardDetail")]
        public async Task<IActionResult> GetDashboardDetail()
        {
            var dashboard = await Mediator.Send(new AdminDashboardDetailsQuery {});
            return Ok(dashboard);
        }
        #endregion

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
        public async Task<IActionResult> GetBenificaryDetail(Guid id, bool isPayment)
        {
            var benificary = await Mediator.Send(new GetBenificariesDetailsQuery
            {
                Id = id,
                IsPayment = isPayment
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

        #region Charity Resources
        [Route("api/Benificary/SaveCharityResources")]
        [HttpPost("SaveCharityResources")]
        public async Task<IActionResult> SaveCharityResources([FromBody] CharityResourcesLookupModel charity)
        {
            var message = await Mediator.Send(new CharityResourceAddUpdateCommand
            {
                charityResources = charity
            });
            return Ok(message);
        }
        [Route("api/Benificary/GetCharityResourcesList")]
        [HttpGet("GetCharityResourcesList")]
        public async Task<IActionResult> GetCharityResourcesList(string searchTerm, int? pageNumber, bool isDropDown)
        {
            var charity = await Mediator.Send(new CharityResourceListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1
            });
            return Ok(charity);
        }

        [Route("api/Benificary/GetCharityResourcesDetail")]
        [HttpGet("GetCharityResourcesDetail")]
        public async Task<IActionResult> GetCharityResourcesDetail(Guid id)
        {
            var charity = await Mediator.Send(new CharityResourceDetailsQuery
            {
                Id = id
            });
            return Ok(charity);
        }

        #endregion

        #region ApprovalPersons
        [Route("api/Benificary/SaveApprovalPersons")]
        [HttpPost("SaveApprovalPersons")]
        public async Task<IActionResult> SaveApprovalPersons([FromBody] ApprovalPersonLookupModel approvalsPerson)
        {
            var message = await Mediator.Send(new ApprovalSystemAddUpdateCommand
            {
                ApprovalsPerson = approvalsPerson
            });
            return Ok(message);
        }
        [Route("api/Benificary/GetApprovalPersonsList")]
        [HttpGet("GetApprovalPersonsList")]
        public async Task<IActionResult> GetApprovalPersonsList(string searchTerm, int? pageNumber, bool isDropDown)
        {
            var approvalPersons = await Mediator.Send(new ApprovalPersonListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1
            });
            return Ok(approvalPersons);
        }

        [Route("api/Benificary/GetApprovalPersonsDetail")]
        [HttpGet("GetApprovalPersonsDetail")]
        public async Task<IActionResult> GetApprovalPersonsDetail(Guid id)
        {
            var approvalPersons = await Mediator.Send(new ApprovalPersonDetailsQuery
            {
                Id = id
            });
            return Ok(approvalPersons);
        }

        #endregion

        #region PaymentTypes
        [Route("api/Benificary/SavePaymentType")]
        [HttpPost("SavePaymentType")]
        public async Task<IActionResult> SavePaymentType([FromBody] PaymentTypeLookupModel payment)
        {
            var message = await Mediator.Send(new PaymentTypeAddUpdateCommand
            {
                Payments = payment
            });
            return Ok(message);
        }
        [Route("api/Benificary/GetPaymentTypeList")]
        [HttpGet("GetPaymentTypeList")]
        public async Task<IActionResult> GetPaymentTypeList(string searchTerm, int? pageNumber, bool isDropDown)
        {
            var payment = await Mediator.Send(new PayementTypeListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1
            });
            return Ok(payment);
        }

        [Route("api/Benificary/GetPaymentTypeDetail")]
        [HttpGet("GetPaymentTypeDetail")]
        public async Task<IActionResult> GetPaymentTypeDetail(Guid id)
        {
            var payment = await Mediator.Send(new PaymentTypeDetailsQuery
            {
                Id = id
            });
            return Ok(payment);
        }

        #endregion

        #region Funds
        [Route("api/Benificary/SaveFunds")]
        [HttpPost("SaveFunds")]
        public async Task<IActionResult> SaveFunds([FromBody] FundsLookupModel funds)
        {
            var message = await Mediator.Send(new FundsAddUpdateCommand
            {
                Funds = funds
            });
            return Ok(message);
        }
        [Route("api/Benificary/GetFundsList")]
        [HttpGet("GetFundsList")]
        public async Task<IActionResult> GetFundsList(string searchTerm, int? pageNumber, bool isDropDown)
        {
            var fund = await Mediator.Send(new FundsListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1
            });
            return Ok(fund);
        }

        [Route("api/Benificary/GetFundsDetail")]
        [HttpGet("GetFundsDetail")]
        public async Task<IActionResult> GetFundsDetail(Guid id)
        {
            var fund = await Mediator.Send(new FundsDetailsQuery
            {
                Id = id
            });
            return Ok(fund);
        }

        #endregion

        #region Payments
        [Route("api/Benificary/SavePayments")]
        [HttpPost("SavePayments")]
        public async Task<IActionResult> SavePayments([FromBody] PaymentLookupModel payment)
        {
            var message = await Mediator.Send(new PaymentsAddUpdateCommand
            {
                Payment = payment
            });
            return Ok(message);
        }
        [Route("api/Benificary/GetPaymentsList")]
        [HttpGet("GetPaymentsList")]
        public async Task<IActionResult> GetPaymentsList(string searchTerm, int? pageNumber)
        {
            var payment = await Mediator.Send(new PaymentListQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1
            });
            return Ok(payment);
        }

        [Route("api/Benificary/GetPaymentsDetail")]
        [HttpGet("GetPaymentsDetail")]
        public async Task<IActionResult> GetPaymentsDetail(Guid id, bool isVoid, bool allowVoid)
        {
            var fund = await Mediator.Send(new PaymentDetailsQuery
            {
                Id = id,
                IsVoid = isVoid,
                AllowVoid = allowVoid
            });
            return Ok(fund);
        }

        #endregion

        #region CharityTransaction
        [Route("api/Benificary/GetCharityTransactionList")]
        [HttpGet("GetCharityTransactionList")]
        public async Task<IActionResult> GetCharityTransactionList(Guid? benificaryId, DateTime? month, DateTime? fromDate, DateTime? toDate)
        {
            var charity = await Mediator.Send(new CharityTransactionListQuery
            {
                BenificayId = benificaryId,
                Month = month,
                FromDate = fromDate,
                ToDate = toDate,
            });
            return Ok(charity);
        }
        #endregion

        #region AutoCode
        [Route("api/Benificary/AutoCodeGenerate")]
        [HttpGet("AutoCodeGenerate")]
        public async Task<IActionResult> AutoCodeGenerate(string name)
        {
            var autoNo = await Mediator.Send(new AutoCodeGenerateQuery
            {
                Name = name,
            });
            return Ok(autoNo);
        }
        #endregion

        #region Imports

      
        [Route("api/Benificary/UploadFilesForImportAuthorize")]
        [HttpPost("UploadFilesForImportAuthorize")]
        public async Task<IActionResult> UploadFilesForImportAuthorize([FromBody] List<AuthorizeVm> rows)
        {
            try
            {
                var list=new List<AuthorizedPerson>();
            foreach (var request in rows)
            {
                 list.Add( new AuthorizedPerson
                {
                    Name = request.Name,
                    AuthorizedPersonCode = Int32.Parse(request.Id),
                    NameAr = request.NameAr,
                    PhoneNo = request.Phone,
                    Address = request.Address,
                    Nationality = request.Nationality,
                    Gender = request.Gender,
                    IqamaNo = request.IqamaNo,
                    PassportNo = request.PassportNo,
                });

            }
            await _Context.AuthorizedPersons.AddRangeAsync(list);
            await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var message = "done";
            return Ok(message); 
        }


        [Route("api/Benificary/UploadFilesForBeneficary")]
        [HttpPost("UploadFilesForBeneficary")]
        public async Task<IActionResult> UploadFilesForBeneficary([FromBody] List<AuthorizeVm> rows)
        {
            try
            {
                var authorizedPerson = _Context.AuthorizedPersons.AsNoTracking().ToList();
                var payment = _Context.PaymentTypes.AsNoTracking().ToList();
                var list = new List<Beneficiaries>();

                foreach (var request in rows)
                {
                    int paymentInterval;
                    if (!int.TryParse(request.Payment_interval, out paymentInterval))
                    {

                    }

                    decimal recurringAmount;
                    if (!decimal.TryParse(request.Recurring_amount, out recurringAmount))
                    {

                    }

                    int authorizedPersonCode;
                    if (!int.TryParse(request.Authorized_person_id, out authorizedPersonCode))
                    {

                    }
                    list.Add(new Beneficiaries
                    {
                        BeneficiaryId = Convert.ToInt32(request.Id),
                        Name = "",
                        PaymentIntervalMonth = paymentInterval,
                        AmountPerMonth = recurringAmount / paymentInterval,
                        RecurringAmount = recurringAmount,
                        UgamaNo = request.Iqama_no,
                        PhoneNo = request.Phone,
                        Note = "",
                        IsActive = request.Isactive == "TRUE",
                        ApprovalPersonId = null,
                        Address = request.Address,
                        AuthorizedPersonId = authorizedPerson.FirstOrDefault(x => x.AuthorizedPersonCode == authorizedPersonCode)?.Id,
                        PaymentTypeId = payment.FirstOrDefault(x => x.Code == paymentInterval)?.Id,
                        NameAr = request.Name,
                        AdvancePayment = 0,
                        DurationType = "Indefinite",
                        ApprovedPaymentId = null,
                        StartDate = null,
                        EndDate = null,
                        StartMonth = DateTime.Parse(request.Stamp_date),
                        IsRegister = authorizedPerson.FirstOrDefault(x => x.AuthorizedPersonCode == authorizedPersonCode) != null

                    });

                }
                await _Context.Beneficiaries.AddRangeAsync(list);
                await _Context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var message = "done";
            return Ok(message);
        }

        [Route("api/Benificary/Payments_Beneficries")]
        [HttpPost("Payments_Beneficries")]
        public async Task<IActionResult> Payments_Beneficries([FromBody] List<AuthorizeVm> rows)
        {
            try
            {
                var authorizedPerson = _Context.AuthorizedPersons.AsNoTracking().ToList();
                var payment = _Context.PaymentTypes.AsNoTracking().ToList();
                var Beneficiaries = _Context.Beneficiaries.AsNoTracking().ToList();
                var list = new List<BenificaryNote>();

                foreach (var request in rows)
                {
                    list.Add(new BenificaryNote
                    {
                        Note = "",
                        Date=Convert.ToDateTime(request.Stamp_date),
                        BenificaryId=Beneficiaries.FirstOrDefault(x=>x.BeneficiaryId==Convert.ToInt32(request.Id))?.Id,

                    }) ;

                }
                await _Context.BenificaryNotes.AddRangeAsync(list);
                await _Context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Ok(null);
        }


        #endregion
    }
}
