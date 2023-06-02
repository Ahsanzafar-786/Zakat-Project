using Focus.Business.Benificary.Models;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Payments.Models;
using Focus.Business.Transactions.Models;
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
        public bool IsPayment { get; set; }
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
                    if(request.IsPayment)
                    {
                        var charity = await Context.CharityTransaction.Where(x => x.BenificayId == request.Id).Select(x => new CharityTransactionLookupModel()
                        {
                            Id = x.Id,
                            DoucmentId = x.DoucmentId,
                            DoucmentCode = x.DoucmentCode,
                            Amount = x.Amount,
                            CharityTransactionDate = x.CharityTransactionDate,
                            DoucmentDate = x.DoucmentDate,
                            Month = x.Month,
                            PaymentMonths = x.Month.Value.Month,
                            Year = x.Year,
                        }).ToListAsync();

                        var query = await Context.Beneficiaries.AsNoTracking().Select(x => new BenificariesLookupModel
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
                            Address = x.Address,
                            ApprovalPersonId = x.ApprovalPersonId,
                            Nationality = x.Nationality,
                            Gender = x.Gender,
                            NameAr = x.NameAr,
                            PassportNo = x.PassportNo,
                            PaymentTypeId = x.PaymentTypeId,
                            RecurringAmount = x.RecurringAmount,
                            StartDate = x.StartDate,
                            FirstMonth = x.StartDate.Value.Month,
                            EndMonth = x.EndDate.Value.Month,
                            EndDate = x.EndDate,
                            Year = x.StartMonth.Value.Year.ToString(),
                            StartMonth = x.StartMonth,
                            ApprovedPaymentId = x.ApprovedPaymentId,
                            DurationType = x.DurationType,
                            AdvancePayment = x.AdvancePayment,
                            PaymentTypeName = x.PaymentTypes.Name,
                            ApprovalPersonName = x.ApprovalPersons.Name,
                            Reason = x.Reason,
                            CharityTransactions = charity,
                            BenificaryAuthorization = x.BenificaryAuthorization.Select(x => new BenificaryAuthorizationLookupModel()
                            {
                                Id = x.Id,
                                BenficaryId = x.BenficaryId,
                                ApprovalPersonId = x.ApprovalPersonId,
                                AuthorizationPersonId = x.AuthorizationPersonId,
                                IsActive = x.IsActive,
                                Date = x.Date.ToString(),
                                Description = x.Description,
                            }).ToList(),
                        }).FirstOrDefaultAsync(x => x.Id == request.Id);

                        if (query == null)
                            throw new NotFoundException("Benificary Not Found", "");


                        return query;
                    }
                    else
                    { 
                        var query = await Context.Beneficiaries.AsNoTracking().Include(x=>x.PaymentTypes).Select(x => new BenificariesLookupModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            BeneficiaryId = x.BeneficiaryId,
                            PaymentType = x.PaymentTypes.Code,
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
                            FirstMonth=x.StartDate.Value.Month,
                            EndMonth = x.EndDate.Value.Month,
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
