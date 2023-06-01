using Focus.Business.CharityResource.Model;
using Focus.Business.CharityResource.Queries;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Transactions.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Focus.Business.Payments.Models;
using Syncfusion.CompoundFile.Net;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Transactions.Queries
{
    public class CharityTransactionListQuery : IRequest<List<CharityTransactionLookupModel>>
    {
        public Guid BenificayId { get; set; }

        public class Handler : IRequestHandler<CharityTransactionListQuery, List<CharityTransactionLookupModel>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<CharityTransactionListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<List<CharityTransactionLookupModel>> Handle(CharityTransactionListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var payments = Context.Payments.Where(x => x.BenificayId == request.BenificayId).Select(x => new PaymentLookupModel()
                    {
                        Id = x.BenificayId,
                    }).ToList();

                    var transactions = await Context.CharityTransaction.ToListAsync(); 

                    var charity = new List<CharityTransactionLookupModel>();

                    foreach (var payment in payments)
                    {
                        var charityTransaction = transactions.FirstOrDefault(x => x.DoucmentId == payment.Id);
                        if(charityTransaction != null)
                        {
                            charity.Add(new CharityTransactionLookupModel()
                            {
                                Id = charityTransaction.Id,
                                DoucmentId = charityTransaction.DoucmentId,
                                DoucmentCode = charityTransaction.DoucmentCode,
                                Amount = charityTransaction.Amount,
                                CharityTransactionDate = charityTransaction.CharityTransactionDate,
                                DoucmentDate= charityTransaction.DoucmentDate,
                                Month= charityTransaction.Month,
                                Year = charityTransaction.Year,
                            });
                        }
                    }

                    return charity;
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
