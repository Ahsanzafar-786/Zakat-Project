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
using DocumentFormat.OpenXml.InkML;

namespace Focus.Business.Transactions.Queries
{
    public class CharityTransactionListQuery : IRequest<List<CharityTransactionLookupModel>>
    {
        public Guid? BenificayId { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

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
                    var query = Context.CharityTransaction
                         .Where(x => x.DocumentName == null && !x.IsVoid)
                         .Select(x => new CharityTransactionLookupModel
                         {
                             Id = x.Id,
                             DoucmentId = x.DoucmentId,
                             DoucmentCode = x.DoucmentCode,
                             Amount = x.Amount,
                             CharityTransactionDate = x.CharityTransactionDate,
                             DoucmentDate = x.DoucmentDate,
                             Month = x.Month,
                             BenificayId = x.BenificayId,
                             Year = x.Year,
                         });

                    if (request.BenificayId.HasValue && request.BenificayId != Guid.Empty)
                    {
                        query = query.Where(x => x.BenificayId == request.BenificayId);
                    }

                    if (request.Month.HasValue)
                    {
                        query = query.Where(x => x.Month.Value.Month == request.Month.Value.Month);
                    }

                    if (request.FromDate.HasValue && request.ToDate.HasValue)
                    {
                        query = query.Where(x => x.Month.Value.Date >= request.FromDate.Value.Date && x.Month.Value.Date <= request.ToDate.Value.Date);
                    }

                    

                    var charity = await query.ToListAsync();

                    var benific = await Context.Beneficiaries
                        .Where(b => charity.Select(c => c.BenificayId).Contains(b.Id))
                        .ToListAsync();

                    foreach (var transaction in charity)
                    {
                        var benificiary = benific.FirstOrDefault(b => b.Id == transaction.BenificayId);
                        transaction.benificaryName = (string.IsNullOrEmpty(benificiary?.Name) ? benificiary?.NameAr : benificiary?.Name);
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
