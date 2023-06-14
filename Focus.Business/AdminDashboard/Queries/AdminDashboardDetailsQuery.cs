using Focus.Business.AdminDashboard.Model;
using Focus.Business.Benificary.Models;
using Focus.Business.Benificary.Queries;
using Focus.Business.Exceptions;
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
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;
using System.Collections;
using DocumentFormat.OpenXml.Bibliography;
using Focus.Domain.Entities;

namespace Focus.Business.AdminDashboard.Queries
{
    public class AdminDashboardDetailsQuery : IRequest<DashboardLookupModel>
    {
        public class Handler : IRequestHandler<AdminDashboardDetailsQuery, DashboardLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<AdminDashboardDetailsQuery> logger, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
            {
                Context = context;
                _logger = logger;
                _userManager = userManager;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<DashboardLookupModel> Handle(AdminDashboardDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = _httpContextAccessor.HttpContext.User;
                    var query = await Context.Beneficiaries.AsNoTracking().Include(x => x.PaymentTypes).ToListAsync();
                    var totalAuthorizePerson = Context.AuthorizedPersons.Count();

                    var totalBenificary = query.Count();
                    var registerBenificary = query.Where(x => x.IsRegister).Count();
                    var unRegisterBenificary = query.Where(x => !x.IsRegister).Count();
                    var oneTimeBenificary = query.Where(x => x.PaymentTypes.Name == "One Time").Count();
                    var monthlyBenificary = query.Where(x => x.PaymentTypes.Name == "1 Month").Count();
                    var totalUser = _userManager.Users.Where(x => x.Code != null && x.CompanyId == user.Identity.CompanyId()).Count();
                    var totalResources = await Context.Funds.AsNoTracking().Select(x => x.Amount).SumAsync();
                    var paymentTypeList = await Context.PaymentTypes.ToListAsync();
                    var paymentWiseBenificaries = new List<BeneficiariesDurationTypeLookUpModel>();
                    if (paymentTypeList != null)
                    {
                        foreach( var paymentType in paymentTypeList)
                        {
                            paymentWiseBenificaries.Add(new BeneficiariesDurationTypeLookUpModel()
                            {
                                PaymentType = paymentType.Name,
                                Indefinate = query.Where(x => x.DurationType == "Indefinite" && x.PaymentTypeId == paymentType.Id).Count(),
                                Customize = query.Where(x => x.DurationType == "Customize" && x.PaymentTypeId == paymentType.Id).Count(),
                            });
                        }
                    }

                 
                    var transactionList = new List<TransactionByMonthLookupModel>();
                    var charityByMonth = await Context.CharityTransaction.AsNoTracking().GroupBy(x => new { x.Month.Value.Month }).Select(g => new { Month = g.Key ,TotalAmount = g.Sum(x => x.Amount) }).ToListAsync();
                    Hashtable monthTable = new Hashtable(){{ "January", 1 },{ "February", 2 },{ "March", 3 },{ "April", 4 },{ "May", 5 },{ "June", 6 },{ "July", 7 },{ "August", 8 },{ "September", 9 },{ "October", 10 },{ "November", 11 },{"December", 12 }};
                    for (int i = 1; i <= 12; i++)
                    {
                        var monthName = monthTable
                            .Keys
                            .OfType<string>()
                            .FirstOrDefault(key => (int)monthTable[key] == i);
                        

                            var transaction = charityByMonth.FirstOrDefault(x => x.Month.Month == i );


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
                        TotalBenificary = totalBenificary,
                        RegisterBenificary = registerBenificary,
                        UnRegisterBenificary = unRegisterBenificary,
                        OneTimeBenificary = oneTimeBenificary,
                        MonthlyBenificary = monthlyBenificary,
                        TotalAuthorizePerson = totalAuthorizePerson,
                        TotalUser = totalUser,
                        TotalResources = totalResources,
                        MonthList=newList,
                        BenificaryPaymentType = paymentWiseBenificaries

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
