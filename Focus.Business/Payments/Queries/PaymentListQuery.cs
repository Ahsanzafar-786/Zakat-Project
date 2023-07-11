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

namespace Focus.Business.Payments.Queries
{
    public class PaymentListQuery : PagedRequest, IRequest<PagedResult<List<PaymentLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public string BeneficiaryName { get; set; }
        public int? Code { get; set; }
        public decimal? Amount { get; set; }
        public int? BenificaryCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? Year { get; set; }
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
                    var userList = _userManager.Users.ToList();

                    var query = Context.Payments.AsNoTracking().Include(x => x.Beneficiaries).Select(x => new PaymentLookupModel
                    {
                        Id = x.Id,
                        BenificayId = x.BenificayId,
                        Amount = x.Amount,
                        Month = x.Month,
                        Year = x.Year,
                        Date = x.Date,
                        Period = x.Period,
                        PaymentCode =x.PaymentCode,
                        BenificaryName = x.Beneficiaries != null ? x.Beneficiaries.Name : "",
                        BenificaryCode = x.Beneficiaries != null ? x.Beneficiaries.BeneficiaryId : 0,
                        BenificaryNameAr = x.Beneficiaries != null ? x.Beneficiaries.NameAr : "",
                        Code = x.Code,
                        IsVoid = x.IsVoid,
                        AllowVoid = x.AllowVoid,
                        Cashier = x.UserId!=null || x.UserId!=""?  userList.FirstOrDefault(y => y.Id == x.UserId)!=null? userList.FirstOrDefault(y => y.Id == x.UserId).FirstName:"" + " ":"" ,
                    }).OrderByDescending(x => x.PaymentCode).ToList();

                    //if (!string.IsNullOrEmpty(request.SearchTerm))
                    //{
                    //    var searchTerm = request.SearchTerm.ToLower();
                    //    query = query.Where(x => x.Amount.ToString().Contains(searchTerm) || x.BenificaryNameAr.Contains(searchTerm) 
                    //                          || x.BenificaryName.Contains(searchTerm) || x.BenificayId.ToString().Contains(searchTerm) || x.Code.ToString().Contains(searchTerm));

                    //}
                    
                     if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        query = query.Where(x => x.BenificaryNameAr.Contains(searchTerm)
                                              || x.BenificaryName.Contains(searchTerm)).ToList(); 

                    }
                     if (request.Amount!=null && request.Amount > 0)
                    {
                        query = query.Where(x => x.Amount == request.Amount).ToList(); ;
                    }
                     if (request.Code != null && request.Code > 0)
                    {
                        query = query.Where(x => x.Code == request.Code).ToList(); ;
                    }
                     if(request.BenificaryCode != null && request.BenificaryCode > 0)
                    {
                        query = query.Where(x => x.BenificaryCode == request.BenificaryCode).ToList(); 
                    }
                    if (request.FromDate.HasValue && request.ToDate.HasValue)
                    {
                        query = query.Where(x => x.Date.Value >= request.FromDate.Value && x.Date.Value <= request.ToDate.Value.AddDays(1)).ToList(); 
                    }
                    if(request.Month != null)
                    {
                        query = query.Where(x => x.Date.Value.Month == request.Month.Value.Month).ToList();
                    }
                    if (request.Year != null)
                    {
                        query = query.Where(x => x.Date.Value.Year == request.Year.Value.Year).ToList();
                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList(); ;


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
