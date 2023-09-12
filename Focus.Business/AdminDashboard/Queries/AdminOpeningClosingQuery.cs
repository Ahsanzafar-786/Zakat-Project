using Focus.Business.AdminDashboard.Model;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Focus.Business.Extensions;
using System.Collections.Generic;

namespace Focus.Business.AdminDashboard.Queries
{
    public class AdminOpeningClosingQuery : IRequest<DashboardLookupModel>
    {
        
        public DateTime? FromDate1 { get; set; }
        public DateTime? ToDate1 { get; set; }

        public class Handler : IRequestHandler<AdminOpeningClosingQuery, DashboardLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger<Handler> _logger; // Change ILogger<AdminOpeningClosingQuery> to ILogger<Handler>

            public Handler(IApplicationDbContext context, ILogger<Handler> logger) // Change ILogger<AdminOpeningClosingQuery> to ILogger<Handler>
            {
                Context = context;
                _logger = logger;
            }

            public async Task<DashboardLookupModel> Handle(AdminOpeningClosingQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var charitytransaction = await Context.CharityTransaction.AsNoTracking().ToListAsync();

                    // Calculate totalFunds
                    decimal totalFunds = charitytransaction
                        .Where(x => x.BenificayId == null && x.CharityTransactionDate.Value.Date >= request.FromDate1.Value.Date && x.CharityTransactionDate.Value.Date <= request.ToDate1.Value.Date)
                        .Sum(x => x.Amount);

                    // Calculate openingBalance
                    decimal openingBalance = charitytransaction
                        .Where(x => x.BenificayId == null && x.CharityTransactionDate.Value.Date < request.FromDate1.Value.Date )
                        .Sum(x => x.Amount) - charitytransaction
                            .Where(x => x.BenificayId != null && x.CharityTransactionDate.Value.Date < request.FromDate1.Value.Date )
                            .Sum(x => x.Amount);
                    


                    //    Calculate benificaryPayment
                    decimal benificaryPayment = charitytransaction
                        .Where(x => x.BenificayId != null && x.CharityTransactionDate.Value.Date >= request.FromDate1.Value.Date && x.CharityTransactionDate.Value.Date <= request.ToDate1.Value.Date)
                        .Sum(x => x.Amount);

                    
                    // Calculate closingBalance
                    decimal closingBalance = openingBalance + totalFunds - benificaryPayment;

                    return new DashboardLookupModel
                    {
                        TotalFunds = totalFunds,
                        BeneficiaryPayment = benificaryPayment,
                        ClosingBalance = closingBalance,
                        OpeningBalance = openingBalance
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "An error occurred while processing the AdminOpeningClosingQuery.");
                    throw new ApplicationException("Something Went Wrong.");
                }
            }
        }
    }
}
