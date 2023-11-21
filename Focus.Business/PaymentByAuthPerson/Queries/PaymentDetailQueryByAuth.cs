using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Payments.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.PaymentByAuthPerson.Queries
{
    public class PaymentDetailQueryByAuth : PagedRequest, IRequest<PagedResult<List<PaymentLookupModel>>>
    {
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
        public class Handler : IRequestHandler<PaymentDetailQueryByAuth, PagedResult<List<PaymentLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentDetailQueryByAuth> logger)
            {
                Context = context;
                _logger = logger;

            }
            public async Task<PagedResult<List<PaymentLookupModel>>> Handle(PaymentDetailQueryByAuth request, CancellationToken cancellationToken)
            {
                try
                {
                  



                    var query = Context.Payments.AsNoTracking()
                             .Where(x=>x.PaymentAuthorizePersonId==request.AuthorizationPersonId && !x.IsVoid)
                                .Include(x => x.Beneficiaries).ThenInclude(x => x.BenificaryAuthorization).ThenInclude(x => x.AuthorizedPerson)
                                .Include(x => x.Beneficiaries).ThenInclude(x => x.PaymentTypes)
                                .Include(x => x.Beneficiaries).ThenInclude(x => x.ApprovalPersons)
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
                                }).OrderByDescending(x => x.Code)
                                .ToList();

                 

                  
                   

                    var pagesize = 100;
                    request.PageSize = pagesize;

                    var count = query.Count();
                    query = query.ToList();


                    return new PagedResult<List<PaymentLookupModel>>
                    {
                        Results = query,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = query.Count / request.PageSize
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
