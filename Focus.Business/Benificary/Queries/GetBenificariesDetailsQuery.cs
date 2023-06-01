using Focus.Business.Benificary.Models;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Benificary.Queries
{
    public class GetBenificariesDetailsQuery : IRequest<BenificariesLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetBenificariesDetailsQuery, BenificariesLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBenificariesDetailsQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<BenificariesLookupModel> Handle(GetBenificariesDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await Context.Beneficiaries.Select(x => new BenificariesLookupModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        BeneficiaryId = x.BeneficiaryId,
                        PaymentIntervalMonth = x.PaymentIntervalMonth,
                        AmountPerMonth = x.AmountPerMonth,
                        UgamaNo = x.UgamaNo,
                        PhoneNo = x.PhoneNo,
                        Note = x.Note,
                        IsActive = x.IsActive,
                        IsRegister = x.IsRegister,
                        AuthorizedPersonId = x.AuthorizedPersonId,
                        Address= x.Address,
                        ApprovalPersonId = x.ApprovalPersonId,
                        Nationality = x.Nationality,
                        Gender = x.Gender,
                        NameAr = x.NameAr,
                        PassportNo= x.PassportNo,
                        PaymentTypeId= x.PaymentTypeId,
                        RecurringAmount= x.RecurringAmount,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        StartMonth = x.StartMonth,
                        ApprovedPaymentId= x.ApprovedPaymentId,
                        DurationType = x.DurationType,
                        AdvancePayment = x.AdvancePayment,
                        PaymentTypeName = x.PaymentTypes.Name,
                        ApprovalPersonName = x.ApprovalPersons.Name,
                        Reason = x.Reason,
                        BenificaryAuthorization = x.BenificaryAuthorization.Select(x => new BenificaryAuthorizationLookupModel()
                        {
                            Id= x.Id,
                            BenficaryId = x.BenficaryId,
                            ApprovalPersonId= x.ApprovalPersonId,
                            AuthorizationPersonId= x.AuthorizationPersonId,
                            IsActive = x.IsActive,
                            Date = x.Date.ToString(),
                            Description = x.Description,
                        }).ToList(),
                    }).FirstOrDefaultAsync(x => x.Id == request.Id);

                    if (query == null)
                        throw new NotFoundException("Benificary Not Found", "");


                    return query;

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Something Went Wrong.");
                }
            }
        }
    }
}
