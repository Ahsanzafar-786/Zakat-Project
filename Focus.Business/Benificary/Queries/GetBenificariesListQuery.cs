using Focus.Business.Benificary.Models;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Benificary.Queries
{
    public class GetBenificariesListQuery : PagedRequest, IRequest<PagedResult<List<BenificariesLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }

        public class Handler : IRequestHandler<GetBenificariesListQuery, PagedResult<List<BenificariesLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBenificariesListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<BenificariesLookupModel>>> Handle(GetBenificariesListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.IsDropDown)
                    {
                        var query = await Context.Beneficiaries.AsNoTracking().Select(x => new BenificariesLookupModel
                        {
                            Id= x.Id,
                            Name = x.Name,
                            NameAr =x.NameAr,
                        }).ToListAsync();

                        return new PagedResult<List<BenificariesLookupModel>>
                        {
                            Results = query
                        };
                    }
                    else
                    {
                        var query = Context.Beneficiaries.AsNoTracking().Include(x => x.AuthorizedPersons).Select(x => new BenificariesLookupModel
                        {
                            Id= x.Id,
                            Name = x.Name,
                            BeneficiaryId = x.BeneficiaryId,
                            PaymentIntervalMonth = x.PaymentIntervalMonth,
                            AmountPerMonth = x.AmountPerMonth,
                            UgamaNo = x.UgamaNo,
                            PhoneNo= x.PhoneNo,
                            IsActive = x.IsActive,
                            IsRegister = x.IsRegister,
                            AuthorizationPersonName = x.AuthorizedPersons.Name,
                            AuthorizedPersonId = x.AuthorizedPersonId,
                            Address = x.Address,
                            ApprovalPersonId = x.ApprovalPersonId,
                            Nationality = x.Nationality,
                            Gender = x.Gender,
                            NameAr = x.NameAr,
                            PassportNo = x.PassportNo,
                            PaymentTypeId = x.PaymentTypeId,
                            RecurringAmount = x.RecurringAmount,
                        }).AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x => x.Name.ToLower().Contains(searchTerm) 
                                                  || x.PhoneNo.Contains(searchTerm) 
                                                  || x.UgamaNo.Contains(searchTerm)
                                                  || x.BeneficiaryId.ToString().Contains(searchTerm));
                        }

                        var count = await query.CountAsync();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var queryList = await query.ToListAsync();

                        return new PagedResult<List<BenificariesLookupModel>>
                        {
                            Results = queryList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = queryList.Count / request.PageSize
                        };

                    }
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
