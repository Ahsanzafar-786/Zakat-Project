using Focus.Business.AdminDashboard.Model;
using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Focus.Business.CharityFunds.Models;
using Focus.Business.Transactions.Models;
using Focus.Business.Payments.Models;

namespace Focus.Business.AdminDashboard.Queries
{
    public class CashierWiseTransaction : IRequest<CashierWiseTransactionLookupModel>
    {
        public Guid? UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class Handler : IRequestHandler<CashierWiseTransaction, CashierWiseTransactionLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<CashierWiseTransaction> logger, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
            {
                Context = context;
                _logger = logger;
                _userManager = userManager;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<CashierWiseTransactionLookupModel> Handle(CashierWiseTransaction request, CancellationToken cancellationToken)
            {
                try
                {
                    var funds = await Context.Funds.AsNoTracking().Where(x => x.UserId == request.UserId.ToString()).Select(x => new FundsLookupModel
                    {
                        UserId = x.UserId,
                        Amount= x.Amount,
                        Id = x.Id
                    }).ToListAsync();
                    var payments = await Context.Payments.AsNoTracking().Where(x => x.UserId == request.UserId.ToString()).Select(x => new PaymentLookupModel()
                    {
                       Id = x.Id,
                    }).ToListAsync();

                    var charityTransaction = await Context.CharityTransaction.AsNoTracking().Where(x => x.CharityTransactionDate.Value.Date >= request.FromDate.Value.Date && x.CharityTransactionDate.Value.Date <= request.ToDate.Value.Date).Select(x => new CharityTransactionLookupModel
                    {
                        Id = x.Id,
                        DoucmentId= x.DoucmentId,
                        CharityTransactionDate  = x.CharityTransactionDate,
                        Amount = x.Amount,
                    }).ToListAsync();

                    var cashierWiseIncomingBalance = new List<CashierWiseIncomingBalanceLookupModel>();
                    var cashierWiseOutgoinBalance = new List<CashierWiseOutgoingBalanceLookupModel>();

                    foreach (var item in payments)
                    {
                        decimal outgoingBalance = 0;

                        var chrities = charityTransaction.Where(x => x.DoucmentId == item.Id).ToList();

                        foreach (var charity in chrities)
                        {
                            decimal Balance = charity.Amount;
                            outgoingBalance = outgoingBalance + Balance;

                            cashierWiseOutgoinBalance.Add(new CashierWiseOutgoingBalanceLookupModel
                            {
                                OutGoingBalance = outgoingBalance,
                                OutGoingDate = charity.CharityTransactionDate?.ToString("dd/MM/yyyy"),
                            });
                        }
                    }

                    foreach (var item in funds)
                    {
                        decimal incomingBalance = 0;

                        var chrities = charityTransaction.Where(x => x.DoucmentId == item.Id).ToList();

                        foreach (var charity in chrities)
                        {
                            decimal Balance = charity.Amount;
                            incomingBalance = incomingBalance + Balance;

                            cashierWiseIncomingBalance.Add(new CashierWiseIncomingBalanceLookupModel
                            {
                                IncomingBalance = incomingBalance,
                                IncomingDate = charity.CharityTransactionDate?.ToString("dd/MM/yyyy"),
                            });
                        }
                    }

                    var cashierWiseTransactionLookupModel = new CashierWiseTransactionLookupModel
                    {
                        CashierWiseIncomingBalance = cashierWiseIncomingBalance,
                        CashierWiseOutgoinBalance = cashierWiseOutgoinBalance,
                    };

                    return cashierWiseTransactionLookupModel;
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
