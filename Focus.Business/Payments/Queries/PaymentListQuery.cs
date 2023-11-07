using Focus.Business.BenificiariesNotes.Model;
using Focus.Business.BenificiariesNotes.Queries;
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
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Domain.Entities;
using Focus.Business.Transactions.Models;

namespace Focus.Business.Payments.Queries
{
    public class PaymentListQuery : PagedRequest, IRequest<PagedResult<List<PaymentLookupModel>>>
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
        public class Handler : IRequestHandler<PaymentListQuery, PagedResult<List<PaymentLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentListQuery> logger, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                _logger = logger;
                _userManager = userManager;

            }
            public async Task<PagedResult<List<PaymentLookupModel>>> Handle(PaymentListQuery request, CancellationToken cancellationToken)
            {
                try
                {
            



                var query = Context.Payments.AsNoTracking();



                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        query = query.Where(x => x.Beneficiaries.NameAr.Contains(searchTerm)
                                                 || x.Beneficiaries.Name.Contains(searchTerm));
                    }
                    if (request.Amount != null && request.Amount > 0)
                    {
                        query = query.Where(x => x.TotalAmount == request.Amount);
                    }
                    if (request.Code != null && request.Code > 0)
                    {
                        query = query.Where(x => x.Beneficiaries.BeneficiaryId == request.Code);
                    }
                    if (request.BenificaryCode != null && request.BenificaryCode > 0)
                    {
                        query = query.Where(x => x.Beneficiaries.BeneficiaryId == request.BenificaryCode);
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
                    //if (request.Register == "Register")
                    //{
                    //    query = query.Where(x => x.IsRegister).To;
                    //}
                    //if (request.Register == "Un-Register")
                    //{
                    //    query = query.Where(x => !x.IsRegister).ToList();
                    //}
                    if (request.UqamaNo != null)
                    {
                        query = query.Where(x => x.Beneficiaries.UgamaNo == request.UqamaNo);
                    }
                    if (request.Nationality != null)
                    {
                        query = query.Where(x => x.Beneficiaries.Nationality == request.Nationality);
                    }
                    if (request.Gender != null)
                    {
                        query = query.Where(x => x.Beneficiaries.Gender == request.Gender);
                    }
                    if (request.ContactNo != null)
                    {
                        query = query.Where(x => x.Beneficiaries.PhoneNo == request.ContactNo);
                    }
                    if (request.ApprovalPersonId != null)
                    {
                        query = query.Where(x => x.Beneficiaries.ApprovalPersonId == request.ApprovalPersonId);
                    }
                    //if (request.AuthorizationPersonId != null)
                    //{
                    //    query = query.Where(x => x.Beneficiaries.AuthorizePersonId == request.AuthorizationPersonId);
                    //}


                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    // Include related data after applying filters and pagination
                    query = query.Include(x => x.Beneficiaries).ThenInclude(x => x.BenificaryAuthorization).ThenInclude(x => x.AuthorizedPerson)
                        .Include(x => x.Beneficiaries).ThenInclude(x => x.PaymentTypes)
                        .Include(x => x.Beneficiaries).ThenInclude(x => x.ApprovalPersons);

                    var results = await query.Select(x => new PaymentLookupModel
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
                    }).ToListAsync(cancellationToken);


                    return new PagedResult<List<PaymentLookupModel>>
                    {
                        Results = results,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = results.Count / request.PageSize
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
