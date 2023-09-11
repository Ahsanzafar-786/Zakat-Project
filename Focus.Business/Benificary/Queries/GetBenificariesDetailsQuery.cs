using Focus.Business.Benificary.Models;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Transactions.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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
                        

                        var charity = await Context.CharityTransaction.Select(x => new CharityTransactionLookupModel()
                        {
                            Id = x.Id,
                            DoucmentId = x.DoucmentId,
                            DoucmentCode = x.DoucmentCode,
                            Amount = x.Amount,
                            BenificayId= x.BenificayId,
                            CharityTransactionDate = x.CharityTransactionDate,
                            DoucmentDate = x.DoucmentDate,
                            Month = x.Month,
                            Years = x.Month.Value.Year.ToString(),
                            PaymentMonths = x.Month.Value.Month,
                            Year = x.Year,
                        }).ToListAsync();
                        var openingBalance = charity.Where(x => x.BenificayId == null).Sum(x => x.Amount) - charity.Where(x => x.BenificayId != null).Sum(x => x.Amount);

                      var  charityPayment= charity.Where(x=>x.BenificayId==request.Id).Select(x => new CharityTransactionLookupModel()
                      {
                          Id = x.Id,
                          DoucmentId = x.DoucmentId,
                          DoucmentCode = x.DoucmentCode,
                          Amount = x.Amount,
                          BenificayId = x.BenificayId,
                          CharityTransactionDate = x.CharityTransactionDate,
                          DoucmentDate = x.DoucmentDate,
                          Month = x.Month,
                          Years = x.Month.Value.Year.ToString(),
                          PaymentMonths = x.Month.Value.Month,
                          Year = x.Year,
                      }).ToList();



                        var query = await Context.Beneficiaries.AsNoTracking().Include(x => x.PaymentTypes).Select(x => new BenificariesLookupModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            BeneficiaryId = x.BeneficiaryId,
                            ApprovalStatus = x.ApprovalStatus,
                            PaymentIntervalMonth = x.PaymentIntervalMonth,
                            AmountPerMonth = x.AmountPerMonth,
                            UgamaNo = x.UgamaNo,
                            PhoneNo = x.PhoneNo,
                            PaymentType = x.PaymentTypes.Code,
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
                            EndYear = x.EndDate.Value.Year.ToString(),
                            StartMonth = x.StartMonth,
                            ApprovedPaymentId = x.ApprovedPaymentId,
                            DurationType = x.DurationType,
                            AdvancePayment = x.AdvancePayment,
                            PaymentTypeName = x.PaymentTypes.Name,
                            ApprovalPersonName = x.ApprovalPersons.Name,
                            Reason = x.Reason,
                            CurrentPaymentMonth = x.CurrentPaymentMonth,
                            CharityTransactions = charityPayment,
                            StartMonthAndYear = x.StartDate.Value.Month.ToString() + " - " + x.StartDate.Value.Year.ToString(),
                            OpeningBalance = openingBalance,
                            BenificaryAuthorization = x.BenificaryAuthorization.Select(x => new BenificaryAuthorizationLookupModel()
                            {
                                Id = x.Id,
                                BenficaryId = x.BenficaryId,
                                ApprovalPersonId = x.ApprovalPersonId,
                                AuthorizationPersonId = x.AuthorizationPersonId,
                                IsActive = x.IsActive,
                                Date = x.Date.ToString(),
                                ApprovalPersonName = x.ApprovalPerson.Name,
                                AuthorizationPersonCode = x.AuthorizedPerson.AuthorizedPersonCode,
                                AuthorizationPersonName = x.AuthorizedPerson.Name,
                                Description = x.Description,
                            }).ToList(),
                        }).FirstOrDefaultAsync(x => x.Id == request.Id);

                        //if (openingBalance == null)
                        //    throw new NotFoundException("Funds not Available", "");

                        if (query == null)
                            throw new NotFoundException("Benificary Not Found", "");
                        if (query != null)
                        {
                            if (query.CurrentPaymentMonth != null && query.EndDate != null)
                            {
                                if (query.DurationType == "Customize")
                                {
                                    if (query.EndDate.Value.Date.Year == query.CurrentPaymentMonth.Value.Date.Year)
                                    {
                                        if (query.EndDate.Value.Date.Month <= query.CurrentPaymentMonth.Value.Date.Month)
                                        {
                                            query.IsCustomize = true;


                                        }



                                    }
                                }
                            }
                        }


                        return query;
                    }
                    else
                    {
                        var charityTransaction = await Context.CharityTransaction.Where(x => x.BenificayId == request.Id).ToListAsync();

                        bool isDisable = false;

                        if (charityTransaction.Count > 0)
                        {
                            isDisable = true;
                        }

                        var query = await Context.Beneficiaries.AsNoTracking()
                            .Include(x=>x.BenificaryAuthorization)
                            .ThenInclude(x=>x.ApprovalPerson)
                            .Include(x => x.BenificaryAuthorization)
                            .ThenInclude(x => x.AuthorizedPerson)
                            .Include(x=>x.PaymentTypes).Select(x => new BenificariesLookupModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            BeneficiaryId = x.BeneficiaryId,
                            ApprovalStatus = x.ApprovalStatus,
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
                            PaymentTypeNameAr = x.PaymentTypes.NameAr,
                            ApprovalPersonName = x.ApprovalPersons.Name,
                            Reason = x.Reason,
                            IsDisable = isDisable,
                            StartMonthAndYear = x.StartDate.Value.Month.ToString() + " - " + x.StartDate.Value.Year.ToString(),
                            BenificaryAuthorization = x.BenificaryAuthorization.Select(x => new BenificaryAuthorizationLookupModel()
                            {
                                Id= x.Id,
                                BenficaryId = x.BenficaryId,
                                ApprovalPersonId= x.ApprovalPersonId,
                                ApprovalPersonName=x.ApprovalPerson.Name==null || x.ApprovalPerson.Name==""? x.ApprovalPerson.NameAr: x.ApprovalPerson.Name,
                                AuthorizationPersonId= x.AuthorizationPersonId,
                                AuthorizationPersonCode = x.AuthorizedPerson.AuthorizedPersonCode,
                                AuthorizationPersonName =x.ApprovalPerson.Name==null||x.AuthorizedPerson.Name==""?x.AuthorizedPerson.NameAr:x.AuthorizedPerson.Name,
                                IsActive = x.IsActive,
                                Date = Convert.ToDateTime(x.Date).ToString("mm/dd/yyyy"),
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
