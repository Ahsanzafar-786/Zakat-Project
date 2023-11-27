using Focus.Business.Benificary.Models;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Benificary.Queries
{
    public class GetBenificaryByAuthorizePerson : PagedRequest, IRequest<PagedResult<List<BenificariesLookupModel>>>
    {
    
        public Guid? AuthorizedPersonId { get; set; }
      

        public class Handler : IRequestHandler<GetBenificaryByAuthorizePerson, PagedResult<List<BenificariesLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBenificaryByAuthorizePerson> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<BenificariesLookupModel>>> Handle(GetBenificaryByAuthorizePerson request, CancellationToken cancellationToken)
            {
                try
                {
                   
                    {
                        var query = Context.Beneficiaries.AsNoTracking()
                            .Include(x => x.AuthorizedPersons)
                            .Include(x => x.PaymentTypes)
                            .Include(x => x.ApprovalPersons)
                            .Where(x => x.IsActive && x.BenificaryAuthorization.Any(y => y.AuthorizationPersonId == request.AuthorizedPersonId))
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
                                ApprovalPersonName = x.ApprovalPersons.Name == null || x.ApprovalPersons.Name == "" ? x.ApprovalPersons.NameAr : x.ApprovalPersons.Name,
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
                                    AuthorizationPersonNameAr = y.AuthorizedPerson.AuthorizedPersonCode + " " + y.AuthorizedPerson.NameAr,

                                }).ToList(),
                            }).OrderBy(x => x.BeneficiaryId).ToList();



                     
                   

                 


                        return new PagedResult<List<BenificariesLookupModel>>
                        {
                            Results = query,
                          
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
