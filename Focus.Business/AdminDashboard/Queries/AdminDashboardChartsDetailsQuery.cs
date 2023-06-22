using Focus.Business.AdminDashboard.Model;
using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.AdminDashboard.Queries
{
    public class AdminDashboardChartsDetailsQuery : IRequest<DashboardLookupModel>
    {
        public class Handler : IRequestHandler<AdminDashboardChartsDetailsQuery, DashboardLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<AdminDashboardChartsDetailsQuery> logger, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
            {
                Context = context;
                _logger = logger;
                _userManager = userManager;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<DashboardLookupModel> Handle(AdminDashboardChartsDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = _httpContextAccessor.HttpContext.User;

                    var transactionList = new List<TransactionByMonthLookupModel>();

                    var selectedYear = DateTime.Now.Year;

                    var charityByMonth = await Context.CharityTransaction.AsNoTracking().Where(x => x.CharityTransactionDate.HasValue && x.CharityTransactionDate.Value.Year == selectedYear)
                       .GroupBy(x => new { x.Month.Value.Month }).Select(g => new { Month = g.Key, TotalAmount = g.Sum(x => x.Amount) }).ToListAsync();


                    //var charityByMonth = await Context.CharityTransaction.AsNoTracking().GroupBy(x => new { x.Month.Value.Month }).Select(g => new { Month = g.Key ,TotalAmount = g.Sum(x => x.Amount) }).ToListAsync();
                    Hashtable monthTable = new Hashtable() { { "January", 1 }, { "February", 2 }, { "March", 3 }, { "April", 4 }, { "May", 5 }, { "June", 6 }, { "July", 7 }, { "August", 8 }, { "September", 9 }, { "October", 10 }, { "November", 11 }, { "December", 12 } };
                    for (int i = 1; i <= 12; i++)
                    {
                        var monthName = monthTable
                            .Keys
                            .OfType<string>()
                            .FirstOrDefault(key => (int)monthTable[key] == i);


                        var transaction = charityByMonth.FirstOrDefault(x => x.Month.Month == i);


                        transactionList.Add(new TransactionByMonthLookupModel
                        {
                            Month = i,
                            MonthName = monthName,
                            Amount = transaction?.TotalAmount ?? 0,

                        });

                    }
                    List<TransactionByMonthLookupModel> newList = new List<TransactionByMonthLookupModel>();
                    foreach (var item in transactionList)
                    {
                        TransactionByMonthLookupModel model = new TransactionByMonthLookupModel
                        {
                            Month = item.Month,
                            MonthName = item.MonthName,
                            Amount = item.Amount
                        };

                        newList.Add(model);
                    }
                    return new DashboardLookupModel
                    {
                        MonthList = newList,                        
                    };
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