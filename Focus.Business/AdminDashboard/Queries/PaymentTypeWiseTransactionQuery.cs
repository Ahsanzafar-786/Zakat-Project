using Focus.Business.AdminDashboard.Model;
using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Focus.Business.AdminDashboard.Queries
{
    public class PaymentTypeWiseTransactionQuery : IRequest<List<TransactionByPaymentTypeLookupModel>>
    {
        public class Handler : IRequestHandler<PaymentTypeWiseTransactionQuery, List<TransactionByPaymentTypeLookupModel>>
        {
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentTypeWiseTransactionQuery> logger, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
            {
                Context = context;
                _logger = logger;
                _userManager = userManager;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<List<TransactionByPaymentTypeLookupModel>> Handle(PaymentTypeWiseTransactionQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var paymentTypeList = await Context.PaymentTypes.ToListAsync();
                    var charitytransaction = await Context.CharityTransaction.AsNoTracking().ToListAsync();
                    var query = await Context.Beneficiaries.AsNoTracking().ToListAsync();

                    var transactionByPaymentTypes = paymentTypeList.Select(item =>
                    {
                        var totalbeneficary = query.Where(x => x.PaymentTypeId == item.Id).ToList();
                        decimal amount = charitytransaction
                            .Where(x => totalbeneficary.Any(bene => bene.Id == x.BenificayId))
                            .Sum(x => x.Amount);

                        return new TransactionByPaymentTypeLookupModel()
                        {
                            PaymentTypeName = item.Name,
                            Amount = amount
                        };
                    }).ToList();

                    return transactionByPaymentTypes;
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
