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
                        BenificaryName = x.Beneficiaries.BeneficiaryId + " " + x.Beneficiaries.Name,
                        BenificaryNameAr = x.Beneficiaries.BeneficiaryId + " " + x.Beneficiaries.NameAr,
                        Code = x.Code,
                        IsVoid = x.IsVoid,
                        AllowVoid = x.AllowVoid,
                        Cashier = _userManager.Users.FirstOrDefault(y => y.Id == x.UserId).FirstName + " " + _userManager.Users.FirstOrDefault(y => y.Id == x.UserId).LastName,
                    }).OrderByDescending(x => x.PaymentCode).AsQueryable();

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
                                              || x.BenificaryName.Contains(searchTerm));

                    }
                     if (request.Amount!=null && request.Amount > 0)
                    {
                        query = query.Where(x => x.Amount == request.Amount);
                    }
                     if (request.Code != null && request.Code > 0)
                    {
                        query = query.Where(x => x.Code == request.Code);
                    }


                    var count =  query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var queryList = await query.ToListAsync();

                    return new PagedResult<List<PaymentLookupModel>>
                    {
                        Results = queryList,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = queryList.Count / request.PageSize
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
