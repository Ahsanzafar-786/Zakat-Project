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
                    var monthlyBenificary = query.Where(x => x.PaymentTypes.Name == "Monthly").Count();
                    var totalUser = _userManager.Users.Where(x => x.Code != null && x.CompanyId == user.Identity.CompanyId()).Count();

                    return new DashboardLookupModel
                    {
                        TotalBenificary = totalBenificary,
                        RegisterBenificary = registerBenificary,
                        UnRegisterBenificary = unRegisterBenificary,
                        OneTimeBenificary = oneTimeBenificary,
                        MonthlyBenificary = monthlyBenificary,
                        TotalAuthorizePerson = totalAuthorizePerson,
                        TotalUser = totalUser,
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
