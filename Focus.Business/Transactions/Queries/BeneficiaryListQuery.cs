using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Benificary.Models;
using Focus.Business.Transactions.Models;
using Microsoft.EntityFrameworkCore;
using Focus.Domain.Entities;

namespace Focus.Business.Transactions.Queries
{
    public class BeneficiaryListQuery : IRequest<List<BenificariesLookupModel>>
    {
        public Guid? AuthorizationPersonId { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public string Registered { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class Handler : IRequestHandler<BeneficiaryListQuery, List<BenificariesLookupModel>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<BeneficiaryListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<List<BenificariesLookupModel>> Handle(BeneficiaryListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var benific = Context.Beneficiaries.AsNoTracking().Include(y=>y.ApprovalPersons).Include(x=>x.BenificaryAuthorization).ThenInclude(z=>z.AuthorizedPerson).ToList();

                    if (request.AuthorizationPersonId != Guid.Empty && request.AuthorizationPersonId != null)
                    {
                        benific = benific
                            .Where(x => x.BenificaryAuthorization.All(y => y.AuthorizationPersonId == request.AuthorizationPersonId))
                            .ToList();
                    }
                    if (request.Registered!=null && request.Registered!="")
                    {
                        bool isRegistered = false;
                        if(request.Registered== "Register")
                        {
                            isRegistered = true;
                        }

                        benific = benific.Where(x => x.IsRegister== isRegistered).ToList();
                    }
                    if (request.ApprovalPersonId != Guid.Empty && request.ApprovalPersonId != null)
                    {
                        benific = benific.Where(x => x.ApprovalPersonId== request.ApprovalPersonId).ToList();
                    }
                    if (request.FromDate != null)
                    {
                        benific.Where(x => x.StartDate == request.FromDate).ToList();
                    }
                    if (request.ToDate != null)
                    {
                        benific = benific.Where(x => x.StartDate == request.ToDate).ToList();
                    }

                    return benific.Select(x=>new BenificariesLookupModel
                    {
                        Name = x.Name==null || x.Name==""?x.NameAr:x.Name ,
                        BeneficiaryId = x.BeneficiaryId,
                        ApprovalStatus = x.ApprovalStatus,
                        PaymentIntervalMonth = x.PaymentIntervalMonth,
                        AmountPerMonth = x.AmountPerMonth,
                        UgamaNo = x.UgamaNo,
                        PhoneNo = x.PhoneNo,
                        IsActive = x.IsActive,
                        IsRegister = x.IsRegister,
                        ApprovalPersonName = x.ApprovalPersons?.Name,
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
                            AuthorizationPersonName = y.AuthorizedPerson?.AuthorizedPersonCode + " " + y.AuthorizedPerson?.Name,
                            AuthorizationPersonNameAr = y.AuthorizedPerson?.AuthorizedPersonCode + " " + y.AuthorizedPerson?.NameAr,

                        }).ToList(),
                    }).ToList();

                    //return charity;
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
