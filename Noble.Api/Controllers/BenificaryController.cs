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
using Focus.Business.Payments.Commands;
using Focus.Business.Payments.Queries;
using Focus.Business.Transactions.Queries;
using System.Collections.Generic;
using System.Linq;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Noble.Api.Models;
using PaymentLookupModel = Focus.Business.Payments.Models.PaymentLookupModel;
using Focus.Business.Reports.Payments.Queries;
using Focus.Persistence.Migrations;
using MailKit.Search;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using NPOI.SS.Formula.Functions;
using DocumentFormat.OpenXml.Wordprocessing;
using Focus.Business.ExpenseCategories.Commands;
using Focus.Business.ExpenseCategories.Model;
using Focus.Business.ExpenseCategories.Queries;
using Focus.Business.Exepenses.Models;
using Focus.Business.Exepenses.Commands;
using Focus.Business.Exepenses.Queries;

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
        public async Task<IActionResult> GetDashboardDetail(Guid? userId)
        {
            var dashboard = await Mediator.Send(new AdminDashboardDetailsQuery
            {
                UserId = userId,
            });
            return Ok(dashboard);
        }
        [Route("api/Benificary/GetDashboardChartsDetail")]
        [HttpGet("GetDashboardChartsDetail")]
        public async Task<IActionResult> GetDashboardChartsDetail(DateTime year)
        {
            var dashboard1 = await Mediator.Send(new AdminDashboardChartsDetailsQuery
            {
                Year = year
            });
            return Ok(dashboard1);
        }

        [Route("api/Benificary/PaymentTypeWiseTransaction")]
        [HttpGet("PaymentTypeWiseTransaction")]
        public async Task<IActionResult> PaymentTypeWiseTransactionQuery()
        {
            var dashboard2 = await Mediator.Send(new PaymentTypeWiseTransactionQuery { });
            return Ok(dashboard2);
        }

        [Route("api/Benificary/OpeningBalanceQuery")]
        [HttpGet("OpeningBalanceQuery")]
        public async Task<IActionResult> OpeningBalanceQuery(DateTime? fromDate1, DateTime? toDate1)
        {
            var dashboard3 = await Mediator.Send(new AdminOpeningClosingQuery {

                FromDate1 = fromDate1,
                ToDate1 = toDate1,

            });
            return Ok(dashboard3);
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
        public async Task<IActionResult> GetBenificaryList(string searchTerm, int? pageNumber, bool isDropDown, string beneficiaryName, string uqamaNo, string beneficiaryId, Guid? authorizationPersonId, Guid? approvalPersonId, string registered, DateTime? fromDate, DateTime? toDate, DateTime? startMonth, DateTime? year,
            decimal? amount, string nationality, string gender, string contact, string status, Guid? paymentType)
        {
            var benificary = await Mediator.Send(new GetBenificariesListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1,
                BeneficiaryName = beneficiaryName,
                UqamaNo = uqamaNo,
                BenificiaryId = beneficiaryId,
                AuthorizedPersonId = authorizationPersonId,
                ApprovalPersonId = approvalPersonId,
                Registered = registered,
                FromDate = fromDate,
                ToDate = toDate,
                StartMonth = startMonth,
                Year = year,
                Amount = amount,
                Nationality = nationality,
                Gender = gender,
                Contact = contact,
                Status = status,
                PaymentType = paymentType,
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
        public async Task<IActionResult> GetBenificaryNoteList(string searchTerm, int? pageNumber, bool isDropDown, string beneficiaryName, string beneficiaryNote, string benificaryCode, string nationalId, string contactNo)
        {
            var benificary = await Mediator.Send(new GetBenificaryNoteListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1,
                BeneficiaryName = beneficiaryName,
                BeneficiaryNote = beneficiaryNote,
                BenificaryCode = benificaryCode,
                NationalId = nationalId,
                ContactNo = contactNo,
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

        [Route("api/Benificary/DeleteBeneficiaryNote")]
        [HttpGet("DeleteBeneficiaryNote")]
        public async Task<IActionResult> DeleteBeneficiaryNote(Guid id)
        {
            var benificary = await Mediator.Send(new DeleteBenificaryNoteCommand
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
        public async Task<IActionResult> GetPaymentsList(string searchTerm, int? pageNumber, string beneficiaryName, int? code, decimal? amount, int? benificaryCode, DateTime? fromDate, DateTime? toDate, DateTime? month
            , DateTime? year, string register, string status, string nationality, string uqamaNo, string gender, string contactNo, Guid? approvalPersonId, Guid? authorizationPersonId)
        {
            var payment = await Mediator.Send(new PaymentListQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
                BeneficiaryName = beneficiaryName,
                Code = code,
                Amount = amount,
                BenificaryCode = benificaryCode,
                FromDate = fromDate,
                ToDate = toDate,
                Month = month,
                Year = year,
                Register = register,
                Status = status,
                Nationality = nationality,
                UqamaNo = uqamaNo,
                Gender = gender,
                ContactNo = contactNo,
                ApprovalPersonId = approvalPersonId,
                AuthorizationPersonId = authorizationPersonId
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


        [Route("api/Benificary/GetPaymentReportQuery")]
        [HttpGet("GetPaymentReportQuery")]
        public async Task<IActionResult> GetPaymentReportQuery(Guid? benificaryId, Guid? userId, DateTime? fromDate, DateTime? toDate)
        {
            var charity = await Mediator.Send(new PaymentReportQuery
            {
                BenificayId = benificaryId,
                UserId = userId,
                FromDate = fromDate,
                ToDate = toDate,
            });
            return Ok(charity);
        }

        [Route("api/Benificary/GetBenificaryReport")]
        [HttpGet("GetBenificaryReport")]
        public async Task<IActionResult> GetBenificaryReport(Guid? authorizationPersonId, Guid? approvalPersonId, string registered, DateTime? fromDate, DateTime? toDate, string searchTerm, string uqamaNo, string beneficiaryId)
        {
            var charity = await Mediator.Send(new BeneficiaryListQuery
            {
                AuthorizationPersonId = authorizationPersonId,
                ApprovalPersonId = approvalPersonId,
                Registered = registered,
                FromDate = fromDate,
                ToDate = toDate,
                SearchTerm = searchTerm,
                UqamaNo = uqamaNo,
                BenificiaryId = beneficiaryId,
            });
            return Ok(charity);
        }

        [Route("api/Benificary/GetTransactionReport")]
        [HttpGet("GetTransactionReport")]
        public async Task<IActionResult> GetTransactionReport(Guid? userId, Guid? benificayId, DateTime? fromDate, DateTime? toDate,DateTime? selectedDate)
        {
            var charity = await Mediator.Send(new TransactionReportQuery
            {
                
                FromDate = fromDate,
                ToDate = toDate,
                BenificayId = benificayId,
                UserId = userId,
                SelectedDate = selectedDate,

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
                var list = new List<AuthorizedPerson>();
                foreach (var request in rows)
                {
                    list.Add(new AuthorizedPerson
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
                    var benificaryAuthorization = new List<BenificaryAuthorization>();

                    var authorize = authorizedPerson.FirstOrDefault(x => x.AuthorizedPersonCode == authorizedPersonCode)
                        ?.Id;
                    if (authorize != null)
                    {
                        benificaryAuthorization.Add(new BenificaryAuthorization
                        {
                            ApprovalPersonId = null,
                            AuthorizationPersonId = authorizedPerson.FirstOrDefault(x => x.AuthorizedPersonCode == authorizedPersonCode)?.Id,
                            IsActive = true,
                            Description = "",
                            Date = DateTime.Parse(request.Stamp_date),
                        });

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
                        IsActive = request.Isactive == "true" ? true : false,
                        ApprovalPersonId = null,
                        Address = request.Address,
                        PaymentTypeId = payment.FirstOrDefault(x => x.Code == paymentInterval)?.Id,
                        NameAr = request.Name,
                        AdvancePayment = 0,
                        DurationType = "Indefinite",
                        ApprovedPaymentId = null,
                        StartDate = null,
                        EndDate = null,
                        StartMonth = DateTime.Parse(request.Stamp_date),
                        BenificaryAuthorization = benificaryAuthorization,
                        IsRegister = authorizedPerson.FirstOrDefault(x => x.AuthorizedPersonCode == authorizedPersonCode) != null,
                     

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

                var Beneficiaries = _Context.Beneficiaries.AsNoTracking().ToList();
                var list = new List<BenificaryNote>();

                foreach (var request in rows)
                {
                    list.Add(new BenificaryNote
                    {
                        Note = request.Note,
                        Date = Convert.ToDateTime(request.Stamp_date),
                        BenificaryId = Beneficiaries.FirstOrDefault(x => x.BeneficiaryId == Convert.ToInt32(request.Beneficiary_id))?.Id,


                    });

                }
                await _Context.BenificaryNotes.AddRangeAsync(list);
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
        [Route("api/Benificary/Funds")]
        [HttpPost("Funds")]
        public async Task<IActionResult> Funds([FromBody] List<AuthorizeVm> rows)
        {
            try
            {

                var list = new List<Funds>();
                var autoNo12 = await Mediator.Send(new AutoCodeGenerateQuery
                {
                    Name = "Funds",
                });

                int i = 0;
                string number = autoNo12;


                foreach (var request in rows)
                {
                    if (i != 0)
                    {
                        string fetchNo = autoNo12.Substring(3);
                        Int32 autoNo = Convert.ToInt32((fetchNo));
                        var format = "00000";
                        autoNo++;
                        string prefix;
                        {
                            prefix = "FU-";
                        }
                        var newCode = prefix + autoNo.ToString(format);
                        autoNo12 = newCode;
                    }
                    i++;
                 

                    list.Add(new Funds
                    {
                        Date = Convert.ToDateTime(request.Stamp_date),
                        Amount = Convert.ToDecimal(request.Amount),
                        Description = request.Check_No,
                        Code=autoNo12,
                        UserId = request.Created_by_id,


                    });

                    var charityTransactions = new CharityTransaction
                    {
                      
                        //DoucmentId = Guid.Parse( request.Id),
                        DoucmentCode  = autoNo12,
                      
                        DocumentName = "Funds",
                   
                        Amount =Convert.ToDecimal( request.Amount),
                        DoucmentDate = DateTime.Parse(request.Stamp_date),
                        CharityTransactionDate = DateTime.Parse(request.Stamp_date),
                        Month = DateTime.Parse(request.Stamp_date),
                        Year = DateTime.Parse(request.Stamp_date).Year.ToString(),
                        UserId = request.Created_by_id,
                      

                    };

                    await _Context.CharityTransaction.AddAsync(charityTransactions);
                 
                }
                await _Context.Funds.AddRangeAsync(list);
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

        [Route("api/Benificary/PaymentsBeneficry")]
        [HttpPost("PaymentsBeneficry")]
        public async Task<IActionResult> PaymentsBeneficry([FromBody] List<AuthorizeVm> rows)
       {
            var Beneficiaries = _Context.Beneficiaries.AsNoTracking().ToList();
            try
            {

                var list = new List<Payment>();

                foreach (var request in rows)
                {

                    int beneficiary_id;
                    if (!int.TryParse(request.Beneficiary_id, out beneficiary_id))
                    {

                    }

                    //list.Add(new Payment
                    //{
                    //    Code = Convert.ToInt32(request.Id),
                    //    BenificayId = Beneficiaries.FirstOrDefault(x => x.BeneficiaryId == beneficiary_id)?.Id,
                    //    Month = Convert.ToDateTime(request.Month),
                    //    MonthName = request.Month,
                    //    Year = request.Year,
                    //    HijriYear = request.Year,
                    //    Amount = Convert.ToDecimal(request.Amount),
                    //    Date = Convert.ToDateTime(request.Stamp_date),
                    //    Period = request.Period,


                    //});

                    var payment = new Payment()
                    {
                        Code = Convert.ToInt32(request.Id),
                        BenificayId = Beneficiaries.FirstOrDefault(x => x.BeneficiaryId == beneficiary_id)?.Id,
                        Month = Convert.ToDateTime(request.Stamp_date),
                        MonthName = request.Month,
                        Year = request.Year,
                        HijriYear = request.Year,
                        Amount = Convert.ToDecimal(request.Amount),
                        Date = Convert.ToDateTime(request.Stamp_date),
                        Period = request.Period,
                        TotalAmount= Convert.ToDecimal(request.Amount)

                    };

                    _Context.Payments.Add(payment);

                    var charityTransaction = new CharityTransaction
                    {
                        DoucmentId = payment.Id,
                        CharityTransactionDate = payment.Date,
                        DoucmentDate = DateTime.Now,
                        DoucmentCode = payment.Code.ToString(),
                        BenificayId = payment.BenificayId,
                        Month = payment.Date,
                        Amount = payment.Amount,
                        Year = payment.Year,
                        HijriYear = payment.HijriYear,
                    };

                    _Context.CharityTransaction.Add(charityTransaction);

                    var query = _Context.Beneficiaries.AsNoTracking().FirstOrDefault(x => x.Id == payment.BenificayId);
                    if (query != null)
                    {
                        query.CurrentPaymentMonth = payment.Month;
                        query.LastPaymentAmount = payment.Amount;
                        _Context.Beneficiaries.Update(query);
                        
                    }



                }
                // await _Context.Payments.AddRangeAsync(list);
                await _Context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(null);

            //var message = "done";
            //return Ok(message);
        }

        [Route("api/Benificary/BeneficaryPaymentUpdate")]
        [HttpGet("BeneficaryPaymentUpdate")]
        public async Task<IActionResult> BeneficaryPaymentUpdate()
        {
            var fund = await Mediator.Send(new BenificariesPaymentReAssignQuery
            {
                
            });
            return Ok(fund);
        }


        #endregion


        #region ExpenseCategory
        [Route("api/Benificary/SaveExpenseCategory")]
        [HttpPost("SaveExpenseCategory")]
        public async Task<IActionResult> SaveExpenseCategory([FromBody] ExpenseCategoryLookupModel expenseCat)
        {
            var message = await Mediator.Send(new ExpenseCategoryAddUpdateCommand
            {
                expenseCategories = expenseCat
            });
            return Ok(message);
        }
        [Route("api/Benificary/GetExpenseCategoryList")]
        [HttpGet("GetExpenseCategoryList")]
        public async Task<IActionResult> GetExpenseCategoryList(string searchTerm, int? pageNumber, bool isDropDown)
        {
            var authorziedPersons = await Mediator.Send(new GetExpenseCategoryListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1
            });
            return Ok(authorziedPersons);
        }

        [Route("api/Benificary/GetExpenseCategoryDetail")]
        [HttpGet("GetExpenseCategoryDetail")]
        public async Task<IActionResult> GetExpenseCategoryDetail(Guid id)
        {
            var authorziedPersons = await Mediator.Send(new GetExpenseCategoryDetailsQuery
            {
                Id = id
            });
            return Ok(authorziedPersons);
        }

        #endregion

        #region Expense
        [Route("api/Benificary/SaveExpense")]
        [HttpPost("SaveExpense")]
        public async Task<IActionResult> SaveExpense([FromBody] ExpenseLookupModel expens)
        {
            var message = await Mediator.Send(new ExpenseAddUpdateCommand
            {
                expense = expens
            });
            return Ok(message);
        }
        [Route("api/Benificary/GetExpenseList")]
        [HttpGet("GetExpenseList")]
        public async Task<IActionResult> GetExpenseList(string searchTerm, int? pageNumber, bool isDropDown)
        {
            var expense = await Mediator.Send(new ExpenseListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropDown,
                PageNumber = pageNumber ?? 1
            });
            return Ok(expense);
        }

        [Route("api/Benificary/GetExpenseDetail")]
        [HttpGet("GetExpenseDetail")]
        public async Task<IActionResult> GetExpenseDetail(Guid id)
        {
            var expense = await Mediator.Send(new ExpenseDetailsQuery
            {
                Id = id
            });
            return Ok(expense);
        }
        [Route("api/Benificary/GetExpenseReport")]
        [HttpGet("GetExpenseReport")]
        public async Task<IActionResult> GetExpenseReport()
        {
            var expense = await Mediator.Send(new ExpenseReportQuery
            {
            });
            return Ok(expense);
        }
        [Route("api/Benificary/GetExpenseSummery")]
        [HttpGet("GetExpenseSummery")]
        public async Task<IActionResult> GetExpenseSummery()
        {
            var Summery = await Mediator.Send(new SummaryReport
            {
            });
            return Ok(Summery);
        }
        #endregion
    }
}
