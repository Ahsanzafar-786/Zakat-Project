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
                    var paymentTypeIds = paymentTypeList.Select(pt => pt.Id).ToList();

                    var transactionByPaymentTypes = paymentTypeIds.Select(paymentTypeId =>
                    {
                        var amount = Context.Beneficiaries
                            .Join(
                                Context.CharityTransaction,
                                beneficiary => beneficiary.Id,
                                charityTransaction => charityTransaction.BenificayId,
                                (beneficiary, charityTransaction) => new { Beneficiary = beneficiary, CharityTransaction = charityTransaction }
                            )
                            .Where(joinResult => joinResult.Beneficiary.PaymentTypeId == paymentTypeId)
                            .Sum(joinResult => joinResult.CharityTransaction.Amount);

                        return new TransactionByPaymentTypeLookupModel()
                        {
                            PaymentTypeName = paymentTypeList.FirstOrDefault(pt => pt.Id == paymentTypeId)?.Name,
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
