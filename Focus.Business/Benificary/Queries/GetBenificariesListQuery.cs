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
        public string BeneficiaryName { get; set; }
        public string UqamaNo { get; set; }
        public string BenificiaryId { get; set; }

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
                            UgamaNo = x.UgamaNo,
                            PhoneNo = x.PhoneNo,
                            ApprovalStatus = x.ApprovalStatus,
                            BeneficiaryId = x.BeneficiaryId
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
                            Id = x.Id,
                            Name = x.Name,
                            BeneficiaryId = x.BeneficiaryId,
                            ApprovalStatus = x.ApprovalStatus,
                            PaymentIntervalMonth = x.PaymentIntervalMonth,
                            AmountPerMonth = x.AmountPerMonth,
                            UgamaNo = x.UgamaNo,
                            PhoneNo = x.PhoneNo,
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
                            Note = x.Note,
                            PaymentTypeId = x.PaymentTypeId,
                            RecurringAmount = x.RecurringAmount,
                            BenificaryAuthorization = x.BenificaryAuthorization.Select(y => new BenificaryAuthorizationLookupModel
                            {
                                Id = y.Id,
                                AuthorizationPersonName = y.AuthorizedPerson.AuthorizedPersonCode + " " + y.AuthorizedPerson.Name,
                                AuthorizationPersonNameAr = y.AuthorizedPerson.AuthorizedPersonCode + " " +  y.AuthorizedPerson.NameAr,

                            }).ToList(),
                        }).OrderByDescending(x => x.BeneficiaryId).AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x => x.Name.ToLower().Contains(searchTerm) 
                                                  || x.NameAr.Contains(searchTerm) );
                        }
                        if (request.UqamaNo != null )
                        {
                            query = query.Where(x => x.UgamaNo == request.UqamaNo);
                        }
                        if (!string.IsNullOrEmpty(request.BenificiaryId)  )
                        {
                            query = query.Where(x => x.BeneficiaryId.ToString() == request.BenificiaryId);
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
