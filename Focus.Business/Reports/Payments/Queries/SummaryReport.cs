using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Reports.Payments.Models;

namespace Focus.Business.Reports.Payments.Queries
{
    public class SummaryReport : IRequest<SummaryReportLookupModel>
    {
  

        public class Handler : IRequestHandler<SummaryReport, SummaryReportLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<SummaryReport> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<SummaryReportLookupModel> Handle(SummaryReport request, CancellationToken cancellationToken)
            {
                try
                {

                    var Transaction =  Context.CharityTransaction;

                    var fundsReceived = await Transaction.Where(x =>  x.BenificayId == null).SumAsync(x => x.Amount);
                    var totalExpense= Context.Expenses.Sum(x => x.Amount);

                    var balance = fundsReceived - totalExpense;

                    var charitydaily = await Transaction.Where(x =>  x.BenificayId != null && x.Beneficiaries.PaymentTypes.Code == 13).SumAsync(x => x.Amount);
                    var charitymonthly = await Transaction.Where(x =>  x.BenificayId != null && x.Beneficiaries.PaymentTypes.Code != 13).SumAsync(x => x.Amount);

                    var totalPayments = charitydaily + charitymonthly;

                    var cashInHand = balance - totalPayments;

                    return new SummaryReportLookupModel
                    {
                        FundsReceived = fundsReceived,
                        TotalExpense = totalExpense,
                        Balance = balance,
                        Charitydaily = charitydaily,
                        Charitymonthly = charitymonthly,
                        TotalPayments = totalPayments,
                        CashInHand = cashInHand,
                      
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
