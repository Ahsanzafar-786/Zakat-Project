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
    public class AdminDashboardDetailsQuery : IRequest<DashboardLookupModel>
    { 
        public Guid? UserId { get; set; }
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
                    var totalAuthorizePerson = Context.AuthorizedPersons.Count();
                    var funds = await Context.Funds.AsNoTracking().ToListAsync();
                    var payments = await Context.Payments.ToListAsync();
                    var paymentTypeList = await Context.PaymentTypes.ToListAsync();
                    var charitytransaction = await Context.CharityTransaction.AsNoTracking().ToListAsync();
                    var query = await Context.Beneficiaries.AsNoTracking().ToListAsync();


                    var totalBenificary = query.Count();
                    var registerBenificary = query.Where(x => x.IsRegister).Count();
                    var unRegisterBenificary = query.Where(x => !x.IsRegister).Count();
                    var totalUser = _userManager.Users.Where(x => x.Code != null && x.CompanyId == user.Identity.CompanyId()).Count();
                    decimal totalIncoming = funds.Sum(x => x.Amount);
                    decimal totalOutgoing = charitytransaction.Sum(x => x.Amount);
                  
                    var totalApprovalPerson = Context.ApprovalPersons.Count();

                    var cashierTotalIncoming = funds.Where(x => x.UserId == request.UserId.ToString()).Sum(x => x.Amount);
                    var paymentUser = payments.Where(x => x.UserId == request.UserId.ToString()).ToList();


                    //decimal cashierTotalOutgoing = 0;
                    //foreach (var item in paymentUser)
                    //{
                    //   var cashierTotalOutgoing1 = charitytransaction.Where(x => x.DoucmentId == item.Id).Sum(x => x.Amount);
                    //   cashierTotalOutgoing = cashierTotalOutgoing + cashierTotalOutgoing1;
                    //}
                    decimal cashierTotalOutgoing = charitytransaction.Join(paymentUser, ct => ct.DoucmentId, pu => pu.Id, (ct, pu) => ct.Amount).Sum();


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

                    


                    return new DashboardLookupModel
                    {
                        TotalBenificary = totalBenificary,
                        RegisterBenificary = registerBenificary,
                        UnRegisterBenificary = unRegisterBenificary,
                        TotalAuthorizePerson = totalAuthorizePerson,
                        TotalUser = totalUser,
                        TotalResources = totalIncoming,
                        TotalOutgoing = totalOutgoing,
                        TotalApprovalPerson = totalApprovalPerson,
                        CashierTotalIncoming = cashierTotalIncoming,
                        CashierTotalOutgoing = cashierTotalOutgoing,
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
