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
        public string NationalId { get; set; }
        public bool IsDropDown { get; set; }
        public string BeneficiaryName { get; set; }
        public string UqamaNo { get; set; }
        public string BenificiaryId { get; set; }
        public Guid? AuthorizedPersonId { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public string Registered { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? StartMonth {get; set;}
        public DateTime? Year { get; set;}
        public decimal? Amount { get; set; }
        public string Nationality { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public Guid? PaymentType { get; set; }

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
                        var query = Context.Beneficiaries.AsNoTracking()
                            .Include(x => x.AuthorizedPersons)
                            .Include(x=>x.PaymentTypes)
                            .Include(x=>x.ApprovalPersons)
                            .Select(x => new BenificariesLookupModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            BeneficiaryId = x.BeneficiaryId,
                            ApprovalStatus = x.ApprovalStatus,
                            PaymentIntervalMonth = x.PaymentIntervalMonth,
                            AmountPerMonth = x.AmountPerMonth,
                            UgamaNo = x.UgamaNo,
                            PaymentTypeName = x.PaymentTypes.Name,
                            PaymentTypeNameAr = x.PaymentTypes.NameAr,
                            ApprovalPersonName = x.ApprovalPersons.Name==null || x.ApprovalPersons.Name == ""? x.ApprovalPersons.NameAr: x.ApprovalPersons.Name,
                            PhoneNo = x.PhoneNo,
                            IsActive = x.IsActive,
                            IsRegister = x.IsRegister,
                            AuthorizationPersonName = x.AuthorizedPersons.Name,
                            AuthorizedPersonId = x.AuthorizedPersonId,
                            Address = x.Address,
                            ApprovalPersonId = x.ApprovalPersonId,
                            ApprovedPaymentId = x.ApprovedPaymentId,
                            StartMonth = x.StartMonth,
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
                                AuthorizationPersonId = y.AuthorizationPersonId,
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

                        //var benific = Context.Beneficiaries.AsNoTracking().Include(y => y.ApprovalPersons).Include(x => x.BenificaryAuthorization).ThenInclude(z => z.AuthorizedPerson).ToList();

                        if (request.AuthorizedPersonId != Guid.Empty && request.AuthorizedPersonId != null)
                        {
                            query = query.Where(x => x.BenificaryAuthorization.Any(y => y.AuthorizationPersonId == request.AuthorizedPersonId));
                        }
                        if (!string.IsNullOrEmpty(request.Registered))
                        {
                            bool isRegistered = request.Registered == "Register" ? true : false;

                            query = query.Where(x => x.IsRegister == isRegistered);
                        }
                        if (request.ApprovalPersonId != Guid.Empty && request.ApprovalPersonId != null)
                        {
                            query = query.Where(x => x.ApprovedPaymentId == request.ApprovalPersonId);
                        }
                        if (request.FromDate.HasValue && request.ToDate.HasValue)
                        {
                            query = query.Where(x => x.StartMonth.Value.Date >= request.FromDate.Value.Date && x.StartMonth.Value.Date <= request.ToDate.Value.Date);
                        }
                        if (request.StartMonth != null)
                        {
                            query = query.Where(x => x.StartMonth.Value.Month == request.StartMonth.Value.Month);
                        }
                        if(request.Year != null)
                        {
                            query = query.Where(x => x.StartMonth.Value.Year == request.Year.Value.Year);
                        }
                        if(request.Amount != null)
                        {
                            query = query.Where(x => x.AmountPerMonth.Equals(request.Amount));
                        }
                        if(request.Contact != null)
                        {
                            query = query.Where(x => x.PhoneNo == request.Contact);
                        }
                        if(request.Nationality != null)
                        {
                            query = query.Where(x => x.Nationality == request.Nationality);
                        }
                        if(request.Gender != null)
                        {
                            query = query.Where(x => x.Gender == request.Gender);
                        }
                        if (!string.IsNullOrEmpty(request.Status))
                        {
                            bool isActive = request.Status == "Active" ? true : false;

                            query = query.Where(x => x.IsActive == isActive);
                        }
                        if (request.PaymentType != null && request.PaymentType!=Guid.Empty)
                        {
                            query = query.Where(x => x.PaymentTypeId== request.PaymentType);
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
