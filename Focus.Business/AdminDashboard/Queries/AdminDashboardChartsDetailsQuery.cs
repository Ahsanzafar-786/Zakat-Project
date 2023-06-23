using DocumentFormat.OpenXml.Bibliography;
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

        public DateTime Year { get; set; }
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

                    DateTime selectedYear = request.Year;

                    if (selectedYear != null)
                    {
                        var charityByMonth = await Context.CharityTransaction.AsNoTracking()
                            .Where(x => x.CharityTransactionDate.HasValue && x.CharityTransactionDate.Value.Year == selectedYear.Year)
                            .GroupBy(x => new { x.CharityTransactionDate.Value.Month })
                            .Select(g => new { Month = g.Key.Month, TotalAmount = g.Sum(x => x.Amount) })
                            .ToListAsync();

                        Hashtable monthTable = new Hashtable()
                          {
                           { 1, "January" }, { 2, "February" }, { 3, "March" }, { 4, "April" }, { 5, "May" }, { 6, "June" },
                           { 7, "July" }, { 8, "August" }, { 9, "September" }, { 10, "October" }, { 11, "November" }, { 12, "December" }
                           };

                        for (int i = 1; i <= 12; i++)
                        {
                            var monthName = (string)monthTable[i];
                            var transaction = charityByMonth.FirstOrDefault(x => x.Month == i);

                            transactionList.Add(new TransactionByMonthLookupModel
                            {
                                Month = i,
                                MonthName = monthName,
                                Amount = transaction?.TotalAmount ?? 0,
                            });
                        }
                    }
                    else
                    {
                        var currentYear = DateTime.Now.Year;
                        var charityByMonth = await Context.CharityTransaction.AsNoTracking()
                            .Where(x => x.CharityTransactionDate.HasValue && x.CharityTransactionDate.Value.Year == currentYear)
                            .GroupBy(x => new { x.CharityTransactionDate.Value.Month })
                            .Select(g => new { Month = g.Key.Month, TotalAmount = g.Sum(x => x.Amount) })
                            .ToListAsync();

                        Hashtable monthTable = new Hashtable()
                           {
                            { 1, "January" }, { 2, "February" }, { 3, "March" }, { 4, "April" }, { 5, "May" }, { 6, "June" },
                            { 7, "July" }, { 8, "August" }, { 9, "September" }, { 10, "October" }, { 11, "November" }, { 12, "December" }
                           };

                        for (int i = 1; i <= 12; i++)
                        {
                            var monthName = (string)monthTable[i];
                            var transaction = charityByMonth.FirstOrDefault(x => x.Month == i);

                            transactionList.Add(new TransactionByMonthLookupModel
                            {
                                Month = i,
                                MonthName = monthName,
                                Amount = transaction?.TotalAmount ?? 0,
                            });
                        }
                    }

                    return new DashboardLookupModel
                    {
                        MonthList = transactionList,
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