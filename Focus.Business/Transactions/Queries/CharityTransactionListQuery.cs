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
using Focus.Domain.Entities;

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
                    var charity = await Context.CharityTransaction.Where(x => x.BenificayId == request.BenificayId).Select(x => new CharityTransactionLookupModel()
                    {
                        Id = x.Id,
                        DoucmentId = x.DoucmentId,
                        DoucmentCode = x.DoucmentCode,
                        Amount = x.Amount,
                        CharityTransactionDate = x.CharityTransactionDate,
                        DoucmentDate = x.DoucmentDate,
                        Month = x.Month,
                        Year = x.Year,
                    }).ToListAsync();

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
