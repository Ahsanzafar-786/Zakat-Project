using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Reports.Payments.Models;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;

namespace Focus.Business.Reports.Payments.Queries
{
    public class PaymentReportQuery : IRequest<PaymentWiseOpeningClosingModel>
    {
        public Guid? BenificayId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string PaymentType { get; set; }

        public class Handler : IRequestHandler<PaymentReportQuery, PaymentWiseOpeningClosingModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentReportQuery> logger, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                _logger = logger;
                _userManager = userManager;
            }
            public async Task<PaymentWiseOpeningClosingModel> Handle(PaymentReportQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    //DateTime openingBalanceDate = request.SelectedDate?.AddDays(-1) ?? DateTime.Now.AddDays(-1);




                    var transaction = Context.CharityTransaction;
                    var requestFromDate = request.FromDate.Value.Date;

                    // Use a single LINQ query to calculate funds and charity, to minimize database calls
                    var transactionSummary = await transaction
                        .GroupBy(x => x.BenificayId == null && x.DocumentName == "Funds")
                        .Select(group => new {
                            IsFund = group.Key,
                            Total = group.Where(x => x.CharityTransactionDate.Value.Date < requestFromDate).Sum(x => x.Amount)
                        })
                        .ToListAsync();

                    var funds = transactionSummary.FirstOrDefault(x => x.IsFund)?.Total ?? 0;
                    var charity = transactionSummary.FirstOrDefault(x => !x.IsFund)?.Total ?? 0;

                    var openingBalance = funds - charity;

                    // Filter the Payments context in a single query to improve performance
                    var paymentsQuery = Context.Payments
                        .Include(x => x.Beneficiaries).ThenInclude(x => x.PaymentTypes)
                        .Include(x => x.SelectedMonth)
                        .AsQueryable();

                    if (request.BenificayId.HasValue && request.BenificayId != Guid.Empty)
                    {
                        paymentsQuery = paymentsQuery.Where(x => x.BenificayId == request.BenificayId);
                    }

                    if (request.UserId.HasValue && request.UserId != Guid.Empty)
                    {
                        paymentsQuery = paymentsQuery.Where(x => x.UserId == request.UserId.ToString());
                    }

                    if (request.PaymentType != null)
                    {
                        paymentsQuery = paymentsQuery.Where(x => x.Beneficiaries.PaymentTypes.Name == request.PaymentType);
                    }

                    if (request.FromDate.HasValue && request.ToDate.HasValue)
                    {
                        var requestToDate = request.ToDate.Value.Date;
                        paymentsQuery = paymentsQuery.Where(x => x.Date != null && x.Date.Value.Date >= requestFromDate && x.Date.Value.Date <= requestToDate);
                    }

                    var paymentList = paymentsQuery
                        .Select(x => new PaymentWiseListLookupModel
                        {
                            Id = x.Id,
                            PaymentId = x.Code.ToString(),
                            Beneficary = x.Beneficiaries != null ? x.Beneficiaries.Id : Guid.Empty,
                            BeneficaryId = x.Beneficiaries != null ? x.Beneficiaries.BeneficiaryId.ToString() : "",
                            BeneficaryName = (x.Beneficiaries.Name == "" || x.Beneficiaries.Name == null) ? x.Beneficiaries.NameAr : x.Beneficiaries.Name,
                            Amount = x.TotalAmount,
                            Note = x.Note,
                            SelectedMonth = x.SelectedMonth.Select(y => y.SelectMonth).ToList(),
                            PaymentType = x.Beneficiaries.PaymentTypes.Name,
                            Date = Convert.ToDateTime(x.Month),
                            PaymentDate = x.Date,
                        })
                        .ToList();

                    var closingBalance = openingBalance - paymentList.Sum(x => x.Amount);

                    return new PaymentWiseOpeningClosingModel
                    {
                        OpeningBalance = openingBalance,
                        PaymentList = paymentList,
                        ClosingBalance = closingBalance
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
