
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Payments.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Focus.Business.Payments.Queries
{
    public class PaymentListQuery : PagedRequest, IRequest<PagedResult<List<PaymentLookupModel>>>
    {
        public bool IsSearchFilter { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string Nationality { get; set; }
        public string UqamaNo { get; set; }
        public string Register { get; set; }
        public string Status { get; set; }
        public string SearchTerm { get; set; }
        public string BeneficiaryName { get; set; }
        public int? Code { get; set; }
        public decimal? Amount { get; set; }
        public int? BenificaryCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? Year { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public Guid? AuthorizationPersonId { get; set; }
        public class Handler : IRequestHandler<PaymentListQuery, PagedResult<List<PaymentLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentListQuery> logger)
            {
                Context = context;
                _logger = logger;

            }
            public async Task<PagedResult<List<PaymentLookupModel>>> Handle(PaymentListQuery request, CancellationToken cancellationToken)
            {
                try
                {


                    //if (!request.IsSearchFilter)
                    //{
                    //    var query1 = Context.Payments.AsNoTracking()
                    //                .Include(x => x.Beneficiaries).ThenInclude(x => x.BenificaryAuthorization).ThenInclude(x => x.AuthorizedPerson)
                    //                .Include(x => x.Beneficiaries).ThenInclude(x => x.PaymentTypes)
                    //                .Include(x => x.Beneficiaries).ThenInclude(x => x.ApprovalPersons)
                    //                .Include(x => x.Beneficiaries).ThenInclude(x => x.CharityTransactions)
                    //                .Include(x => x.ApplicationUser)
                                     

                    //                .Select(x => new PaymentLookupModel
                    //                {
                    //                    Id = x.Id,
                    //                    BenificayId = x.BenificayId,
                    //                    Amount = x.Amount,
                    //                    Month = x.Month,
                    //                    Year = x.Year,
                    //                    Date = x.Date,
                    //                    Period = x.Period,
                    //                    PaymentCode = x.PaymentCode,
                    //                    BenificaryName = x.Beneficiaries.Name,
                    //                    BenificaryCode = x.Beneficiaries.BeneficiaryId,
                    //                    BenificaryNameAr = x.Beneficiaries.NameAr,
                    //                    Note = x.Note,
                    //                    Code = x.Code,
                    //                    IsVoid = x.IsVoid,
                    //                    TotalAmount = x.TotalAmount,
                    //                    AllowVoid = x.AllowVoid,
                    //                    PaymentByAuthorizePerson = x.PaymentByAuthorizePerson,
                    //                    IsRegister = x.Beneficiaries.IsRegister,
                    //                    Nationality = x.Beneficiaries.Nationality,
                    //                    UgamaNo = x.Beneficiaries.UgamaNo,
                    //                    Gender = x.Beneficiaries.Gender,
                    //                    ContactNo = x.Beneficiaries.PhoneNo,
                    //                    DurationType = x.Beneficiaries.DurationType,
                    //                    LastPaymentAmount = x.Beneficiaries.CharityTransactions.Sum(x => x.Amount),

                    //                    LastPaymentDate = x.Beneficiaries.CharityTransactions.OrderBy(x => x.DoucmentDate).LastOrDefault(charity => charity.DoucmentId == x.Id).Month,
                    //                    ApprovalPersonId = x.Beneficiaries.ApprovedPaymentId,
                    //                    NextMonth = x.Beneficiaries.CurrentPaymentMonth.Value.AddMonths(1),
                    //                    EndMonth = x.Beneficiaries.EndDate,
                    //                    ApprovalPersonName = x.Beneficiaries.ApprovalPersons.Name,
                    //                    PaymentType = x.Beneficiaries.PaymentTypes.Name,
                    //                    PaymentTypeAr = x.Beneficiaries.PaymentTypes.NameAr,
                    //                    AuthorizePersonId = x.Beneficiaries.BenificaryAuthorization != null ? x.Beneficiaries.BenificaryAuthorization.FirstOrDefault().AuthorizationPersonId : null,
                    //                    AuthorizePersonName = x.Beneficiaries.BenificaryAuthorization != null ? x.Beneficiaries.BenificaryAuthorization.FirstOrDefault().AuthorizedPerson.Name : null,

                    //                    Cashier = x.ApplicationUser.UserName,
                    //                })
                    //                .Where(x => !x.IsVoid && !x.PaymentByAuthorizePerson && x.PaymentType!= "Daily Payment")

                    //                .OrderByDescending(x => x.Code).Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    //    var count1 = Context.Payments.AsNoTracking().Count();


                    //    return new PagedResult<List<PaymentLookupModel>>
                    //    {
                    //        Results = query1,
                    //        RowCount = count1,
                    //        PageSize = request.PageSize,
                    //        CurrentPage = request.PageNumber,
                    //        PageCount = count1 / request.PageSize
                    //    };

                    //}


                    var query = Context.Payments.AsNoTracking()
                        .Include(x => x.Beneficiaries).ThenInclude(x => x.BenificaryAuthorization).ThenInclude(x => x.AuthorizedPerson)
                        .Include(x => x.Beneficiaries).ThenInclude(x => x.PaymentTypes)
                        .Include(x => x.Beneficiaries).ThenInclude(x => x.ApprovalPersons)
                        .Include(x => x.Beneficiaries).ThenInclude(x => x.CharityTransactions)
                        .Include(x => x.ApplicationUser)

                                .Select(x => new PaymentLookupModel
                                {
                                    Id = x.Id,
                                    BenificayId = x.BenificayId,
                                    Amount = x.Amount,
                                    Month = x.Month,
                                    Year = x.Year,
                                    Date = x.Date,
                                    Period = x.Period,
                                    PaymentCode = x.PaymentCode,
                                    BenificaryName = x.Beneficiaries.Name,
                                    BenificaryCode = x.Beneficiaries.BeneficiaryId,
                                    BenificaryNameAr = x.Beneficiaries.NameAr,
                                    Note = x.Note,
                                    Code = x.Code,
                                    PaymentByAuthorizePerson = x.PaymentByAuthorizePerson,

                                    IsVoid = x.IsVoid,
                                    TotalAmount = x.TotalAmount,
                                    AllowVoid = x.AllowVoid,
                                    IsRegister = x.Beneficiaries.IsRegister,
                                    Nationality = x.Beneficiaries.Nationality,
                                    UgamaNo = x.Beneficiaries.UgamaNo,
                                    Gender = x.Beneficiaries.Gender,
                                    ContactNo = x.Beneficiaries.PhoneNo,
                                    DurationType = x.Beneficiaries.DurationType,
                                    LastPaymentAmount = x.Beneficiaries.CharityTransactions.Sum(x => x.Amount),

                                    LastPaymentDate = x.Beneficiaries.CharityTransactions.OrderBy(x => x.DoucmentDate).LastOrDefault(charity => charity.DoucmentId == x.Id).Month,
                                    ApprovalPersonId = x.Beneficiaries.ApprovedPaymentId,
                                    NextMonth = x.Beneficiaries.CurrentPaymentMonth.Value.AddMonths(1),
                                    EndMonth = x.Beneficiaries.EndDate,
                                    ApprovalPersonName = x.Beneficiaries.ApprovalPersons.Name,
                                    PaymentType = x.Beneficiaries.PaymentTypes.Name,
                                    PaymentTypeAr = x.Beneficiaries.PaymentTypes.NameAr,
                                    AuthorizePersonId = x.Beneficiaries.BenificaryAuthorization != null ? x.Beneficiaries.BenificaryAuthorization.FirstOrDefault().AuthorizationPersonId : null,
                                    AuthorizePersonName = x.Beneficiaries.BenificaryAuthorization != null ? x.Beneficiaries.BenificaryAuthorization.FirstOrDefault().AuthorizedPerson.Name : null,

                                    Cashier = x.ApplicationUser.UserName,
                                })
                                 .Where(x => !x.IsVoid && !x.PaymentByAuthorizePerson && x.PaymentType != "Daily Payment")

                                .OrderByDescending(x => x.Code).AsQueryable();



                    //}
                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        query = query.Where(x => x.BenificaryNameAr.Contains(searchTerm)
                                              || x.BenificaryName.Contains(searchTerm));

                    }
                    if (request.Amount != null && request.Amount > 0)
                    {
                        query = query.Where(x => x.TotalAmount == request.Amount);
                    }
                    if (request.Code != null && request.Code > 0)
                    {
                        query = query.Where(x => x.BenificaryCode == request.Code);
                    }
                    if (request.BenificaryCode != null && request.BenificaryCode > 0)
                    {
                        query = query.Where(x => x.BenificaryCode == request.BenificaryCode);
                    }
                    if (request.FromDate.HasValue && request.ToDate.HasValue)
                    {
                        query = query.Where(x => x.Date.Value.Date >= request.FromDate.Value.Date && x.Date.Value.Date <= request.ToDate.Value.Date);
                    }
                    if (request.Month != null)
                    {
                        query = query.Where(x => x.Date.Value.Month == request.Month.Value.Month && x.Date.Value.Year == request.Month.Value.Year);
                    }
                    if (request.Year != null)
                    {
                        query = query.Where(x => x.Date.Value.Year == request.Year.Value.Year);
                    }
                    if (request.Register == "Register")
                    {
                        query = query.Where(x => x.IsRegister);
                    }
                    if (request.Register == "Un-Register")
                    {
                        query = query.Where(x => !x.IsRegister);
                    }
                    if (request.UqamaNo != null)
                    {
                        query = query.Where(x => x.UgamaNo == request.UqamaNo);
                    }
                    if (request.Nationality != null)
                    {
                        query = query.Where(x => x.Nationality == request.Nationality);
                    }
                    if (request.Gender != null)
                    {
                        query = query.Where(x => x.Gender == request.Gender);
                    }
                    if (request.ContactNo != null)
                    {
                        query = query.Where(x => x.ContactNo == request.ContactNo);
                    }
                    if (request.ApprovalPersonId != null)
                    {
                        query = query.Where(x => x.ApprovalPersonId == request.ApprovalPersonId);
                    }
                    if (request.AuthorizationPersonId != null)
                    {
                        query = query.Where(x => x.AuthorizePersonId == request.AuthorizationPersonId);
                    }

                    var pagesize = 100;
                    request.PageSize = pagesize;

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                    var record = query.ToList();

                    return new PagedResult<List<PaymentLookupModel>>
                    {
                        Results = record,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = record.Count / request.PageSize
                    };

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
